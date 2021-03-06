﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Xenial.Framework.Utils.Slugger
{
    /// <summary>   Class Slugifier. Implements the <see cref="ISlugify" /> </summary>
    ///
    /// <seealso cref="ISlugify">   <autogeneratedoc /> </seealso>

    public class Slugifier : ISlugify
    {
        /// <summary>   Gets or sets the configuration. </summary>
        ///
        /// <value> The configuration. </value>

        protected SlugifierConfig Config { get; set; }

        /// <summary>   Initializes a new instance of the <see cref="Slugifier"/> class. </summary>
        public Slugifier() : this(new SlugifierConfig()) { }

        /// <summary>   Initializes a new instance of the <see cref="Slugifier"/> class. </summary>
        ///
        /// <param name="config">   The configuration. </param>

        public Slugifier(SlugifierConfig config)
            => Config = config ?? throw new ArgumentNullException(nameof(config), "can't be null use default config or empty constructor.");

        /// <summary>   Implements <see cref="ISlugify.GenerateSlug(string)"/> </summary>
        ///
        /// <param name="inputString">  The string to slugify. </param>
        ///
        /// <returns>   A slugified version of <paramref name="inputString"/> </returns>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1308:Use ToUpper instead of ToLower", Justification = "By design")]
        public string GenerateSlug(string inputString)
        {
            inputString ??= string.Empty;
            if (Config.ForceLowerCase)
            {
                inputString = inputString.ToLowerInvariant();
            }

            if (Config.TrimWhitespace)
            {
                inputString = inputString.Trim();
            }

            inputString = CleanWhiteSpace(inputString, Config.CollapseWhiteSpace);
            inputString = ApplyReplacements(inputString, Config.StringReplacements);
            inputString = RemoveDiacritics(inputString);
            inputString = DeleteCharacters(inputString, Config.DeniedCharactersRegex);

            if (Config.CollapseDashes)
            {
                inputString = Regex.Replace(inputString, "--+", "-");
            }

            return inputString;
        }

        /// <summary>   Cleans the white space. </summary>
        ///
        /// <param name="str">      The string. </param>
        /// <param name="collapse"> if set to <c>true</c> [collapse]. </param>
        ///
        /// <returns>   System.String. </returns>

        protected virtual string CleanWhiteSpace(string str, bool collapse)
            => Regex.Replace(str, collapse ? @"\s+" : @"\s", " ");

        // Thanks http://stackoverflow.com/a/249126!

        /// <summary>   Removes the diacritics. </summary>
        ///
        /// <param name="str">  The string. </param>
        ///
        /// <returns>   System.String. </returns>

        protected virtual string RemoveDiacritics(string str)
        {
            str ??= string.Empty;
            var stFormD = str.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var ich = 0; ich < stFormD.Length; ich++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>   Applies the replacements. </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="str">          The string. </param>
        /// <param name="replacements"> The replacements. </param>
        ///
        /// <returns>   System.String. </returns>

        protected virtual string ApplyReplacements(string str, IDictionary<string, string> replacements)
        {
            _ = replacements ?? throw new ArgumentNullException(nameof(replacements));

            var sb = new StringBuilder(str);

            foreach (var replacement in replacements)
            {
                sb = sb.Replace(replacement.Key, replacement.Value);
            }

            return sb.ToString();
        }

        /// <summary>   Deletes the characters. </summary>
        ///
        /// <param name="str">      The string. </param>
        /// <param name="regex">    The regex. </param>
        ///
        /// <returns>   System.String. </returns>

        protected virtual string DeleteCharacters(string str, string regex)
            => Regex.Replace(str, regex, "");
    }
}

