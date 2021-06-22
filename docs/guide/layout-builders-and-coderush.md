---
title: DetailViewLayoutBuilders and CodeRush
---
# LayoutBuilders and CodeRush

## Introduction

The purpose of this section is to examine in a more forensic fashion the way that 'DetailViewLayoutBuilders' are constructed and in doing so examine ways in which the process of building them can be enhanced with the aid of [CodeRush](https://www.devexpress.com/products/coderush/).

As stated in the Overview to the documentation Xenial.Framework has been designed to work with the DevExpress expressApplicationFramework (XAF).  At the time of writing this documentation a DevExpress 'Ultimate' package was a pre-requisite in order to use XAF.  As CodeRush is also included in the 'Ultimate' package there is no additional cost to using it in combination with Xenial.Framework and as will become clear a great many benefits.

On completion of this section you should have a better understanding of the default XAF behaviour in relation to the construction of Detail View layouts, the way in which Xenial.Framework utilises XAF's inbuilt layout building mechanism and the ways in which CodeRush can be used to speed up the construction of Detail View layouts.

Throughout the section extensive use will be made of a small model.  All of the screenshots were taken within Visual Studio 2019 Enterprise.  Whilst the use of Visual Studio is not mandatory for Xenial.Framework it is required for CodeRush (this requirement may change in the future but at the time of writing was very much the case).

The model is derived from a base class used for all business objects in a particular application.  Its purpose is to provide rather simple, almost crude, basic auditing for that application.  Very specifically it records exactly when a business object was created and by whom (there is an assumption here that basic XAF security is in use) and when and if the object was last modified.  The mechanics behind the construction of this base class and the peripheral bits that are needed to provide the end user with easy access to this information are beyond the scope of this documentation however the actual presentation of that information is very relevant.

There are four simple properties that the end user will need to be able to see;

``` cs
        [ModelDefault("AllowEdit", "False")]
        public string CreatedBy
        {
            get => createdBy;
            set
            {
                if(createdBy == value)
                    return;
                createdBy = value;
                OnPropertyChanged();
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "{0:F}")]
        public DateTime? CreatedOn
        {
            get => createdOn;
            set
            {
                if(createdOn == value)
                    return;
                createdOn = value;
                OnPropertyChanged();
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public string ModifiedBy
        {
            get => modifiedBy;
            set
            {
                if(modifiedBy == value)
                    return;
                modifiedBy = value;
                OnPropertyChanged();
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "{0:F}")]
        public DateTime? ModifiedOn
        {
            get => modifiedOn;
            set
            {
                if(modifiedOn == value)
                    return;
                modifiedOn = value;
                OnPropertyChanged();
            }
        }

```

::: tip INFORMATION
The properties illustrated above are from an XAF Domain Component, hence the OnPropertyChanged() within the setter. A Domain Component is being used because the information will be presented to the user via a PopupWindowShowAction as will become apparent.
:::

## XAF Default Behaviour

Purely default behaviour will lead XAF to create the following Detail View for these properties.

![lbcr1 ](/images/guide/layout-builders/lbcr1.png)

What has just happened here?  In essence XAF has cycled through the properties in the model from top to bottom.  For each one it has created a property editor and then drawn that editor onto the form.  In reality what has been presented is perfectly acceptable but it masks an issue.  

To illustrate the problem consider the following properties from a Customer model.

``` cs
        public string Address { get => address; set => SetPropertyValue(nameof(Address), ref address, value); }

        public string CustomerName
        {
            get => customerName;
            set => SetPropertyValue(nameof(CustomerName), ref customerName, value);
        }

        public string Email { get => email; set => SetPropertyValue(nameof(Email), ref email, value); }

        public string FirstName { get => firstName; set => SetPropertyValue(nameof(FirstName), ref firstName, value); }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public string LastName { get => lastName; set => SetPropertyValue(nameof(LastName), ref lastName, value); }
```

And then the resulting default Detail View.

![lbcr2 ](/images/guide/layout-builders/lbcr2.png)

The created view is functional but hardly user friendly, some work will need to be done in order to create a customer input form that a data entry clerk could process at speed.  Notice too that as the number of properties in a model grows the default behaviour changes from creating a view with a single column  to one with two (or more if necessary).  The forms will be functional but that is probably the best that can be said for them.  

Collections, typically but not exclusively relations in database parlance, would be represented as separate tabs at the bottom of the created view.

::: tip INFORMATION
By default layouts are constructed from top to bottom, starting with single properties in the order in which they appear in the model's class (laid out vertically) and split into columns as the number of those properties grow.  Once all single properties have been accounted for collections are drawn in tabs at the bottom, again in the order in which they are found in the model's class.
:::

## Creating a Detail View with the DetailViewLayoutBuilder

Having established what XAF does by default when constructing Detail Views and that a degree of customisation will be necessary for all but the very simplest of forms it's time to examine how Xenial.Framework's `DetailViewLayoutBuilder` can be used to facilitate this process. You should by now be familiar with the different approaches that Xenial.Framework provides for the construction of detail view layouts.  It is time to examine building a detail view layout from scratch using Xenial.Framework.  Using the first example of the simple audit properties close attention will be paid to the way a detail view is constructed and how the process can be automated with CodeRush.

The first walk through will use Xenial.Framework's LayoutBuilder of T syntax.


The first task is to add the [DetailViewLayoutBuilder] attribute to the class , which in turn will require the addition of a using statement., in all around about forty characters.  A simple CodeRush template could reduce that considerably. 


::: tip INFORMATION
Template creation in CodeRush isn't difficult but does require practice. Illustrations that follow will show the basics of what is being done but they are no substitute for proper consultation of the CodeRush documentation itself.  
:::


In the illustration below is the first template that will be used.  It's job is to provide the required Class attribute and necessary using statement, in fact it will be used to provide an additional using statement that will be required later on as well.


![lbcr3 ](/images/guide/layout-builders/lbcr3.png)


The template is expanded by typing xdv followed by the spacebar.



![lbcrgif1 ](/images/guide/layout-builders/lbcrgif1.gif)



Although the layout created by default was acceptable it could be clearer.  Separating the information for when the object was created from when it was modified would provide a clear differentiation between the two pieces of information. To begin with the basic framework for a DetailViewLayoutBuilder needs to be set in the code and it will look like this;

```cs
        public static Layout BuildLayout()
        {
            var b = new LayoutBuilder<CreatedOrModified>();
            return new Layout
            {
               //Code to construct the layout itself will go here
            };
        }
```

This ought to be perfect fodder for a template particularly as CodeRush has an inbuilt solution for resolving the class name.


![lbcr5 ](/images/guide/layout-builders/lbcr5.png)


The following points are worthy of note.  The special term `«Class»` will resolve the reference to T and `«Caret»` indicates where we want the caret to end up to allow the immediate continuation of code writing to complete the layout.  The template will need to be expanded within the body of the class where the layout builder will be created in order for the class to be resolved, hence the [InClass] context.  Finally a note about the template mnemonic xenlbdv.  xen for Xenial, lb for LayoutBuilder and dv for Detail View.


In use this is what happens.


![lbcrgif2 ](/images/guide/layout-builders/lbcrgif2.gif)


Attention now turns to the actual layout. Remember that layouts are built from the top down. The first scetion that will be required is for the information about when the object was created and by whom.  This will require a group box to display a couple of property editors. Displaying the editors vertically will be perfectly acceptable so a vertical Group  is required.  The code for that is shown below.

``` cs
 b.VerticalGroup(
    g =>
    {
        g.Caption = "Created By";
        g.ShowCaption = true;
    },
    // Code to create the property editors will follow on here
 )
```

::: tip INFORMATION
As this pice of isolated code is a little out of context on its own it is worth remembering that b represents the new LayoutBuilder that was created in the previous step.
:::


Once again this lends itself perfectly to a CodeRush template.


![lbcr6 ](/images/guide/layout-builders/lbcr6.png)


Once again there are some points to note in this template.  `«Caret»«FieldStart»(Add Caption)«FieldEnd»«BlockAnchor»` is instructing the template that that is where one wishes to start typing information ( the (Add Caption) part provides a little aide memoir as to what information needs to be entered). `«FinalTarget»` tells the template where the caret needs to end up. As for the template mnemonic xen for Xenial, lb for LayoutBuilder, dv for DetailView and vg for Vertical Group.


::: tip 
At this point it should be relatively easy to discern that it would only take a slight adjustment in the template for it to be used to create a horizontal group.
:::


The next step is to add the two property editors that will be required.  This will require the following code;


``` cs
b.PropertyEditor(m => m.CreatedBy),
b.PropertyEditor(m => m.CreatedOn),
```

At first glance this may not seem quite so simple to turn into a template but remember that the LayoutBuilder has been designed to make use of intellisense.



![lbcr7 ](/images/guide/layout-builders/lbcr7.png)


`«Caret»«FieldStart»(Add Property)«FieldEnd»«BlockAnchor»` should be familiar, although note that the aide memoir has been changed to reflect the fact that a property name is required.  Although not instantly obvious from the illustration note also that that section of the template is replacing the '.CreatedBy' section.  That means that if the first thing typed is a period (.) then intellisense will kick in and provide the property names. `«FieldStart»(Add Property)«FieldEnd»` tells the template that that is the next place upon which to alight the cursor.


::: tip 
It shouldn't be difficult to see from this how to structure a template to cope with one or several property editors.
:::

That should result in a completed vertical group box containing two property editors.

``` cs
        public static Layout BuildLayout()
        {
            var b = new LayoutBuilder<CreatedOrModified>();
            return new Layout
            {
                b.VerticalGroup(
                g =>
                {
                    g.Caption = "Created By";
                    g.ShowCaption = true;
                },
                b.PropertyEditor(m => m.CreatedBy),
                b.PropertyEditor(m => m.CreatedOn)
                )  
                //Further groups or tabs and property editors could be added here   
            };
        }
```


Repeating the template expansions would very quickly produce the final layout required.



## Applying some Lateral Thinking


You should by now have a reasonable sense for the power of Xenial.Framework's LayoutBuilders and you should be starting to develop an idea of what can be achieved with some well crafted CodeRush templates.  Now comes the time to start putting the two together in such a way that team members, or even individuals can start to 'guess' their way along.

The stumbling block for many people when it comes to using CodeRush it trying to memorise all of the template expansions that there are, let alone additional ones!.  Part of the strength that lies with CodeRush is the clever use that has been made of its mnemonic naming structure for templates, so how might that be transferred to Layout Builders in such a way as intelligent guesswork will get you to the template that you need and in as short a time as possible.


### A Mnemonic Naming Structure



Clearly all templates should have a starting point and x for Xenial seems appropriate.

As it's LayoutBuilders that are the current subject of interest then l for Layout Builder.

Detail Views are actually being built so d for Detail View

That provides a starting point for all templates that create DetailViewLayoutBuilders   xld.

Detail Views have the following containers, Vertical Groups, Horizontal Groups and Tabs, v,h and t respectively.

Property Editors are found in both the main view and within containers, p would seem obvious.

Lastly there can be an empty space element that helps with forming the layout, e seems a good candidate.

A simple number following v,h,t,p or e would indicate how many were needed.


### Combining Templates


The other great strength of CodeRush is the way in which templates can be combined.  A very large portion of LayoutBuilder code could be described, quite legitimately, as boiler plate making it perfect for templates as very little actually needs to be manually added.

Returning to the simple audit view.  It was decided that there should be two vertical groups (each with a caption) and in each group there would be two property editors.  Not specifically mentioned was that each group would also require an empty space item.

Using the logic of the naming structure that was previously described the template for that could be guessed as being;

`xldvp2evp2e`

the template itself would look like this;



![lbcr8 ](/images/guide/layout-builders/lbcr8.png)



On expansion that just leaves six key pieces of information to be added, the two caption for the vertical groups and triggering the intellisense of four occasions to add the required property names.  All the boiler plate code is there but there has been no need to type it out.



## Summary


By now you should have gained an appreciation of how the `Xenial.Framework` constructs `DetailViewLayoutBuilders`.  You should be able to appreciate how full use has been made of the featureset contained within the C# language and the use that can be made of intellisense as detail views are created.  Alongside that you should also have gained an appreciation as to how CodeRush can be used to simplify the process by eliminating most , if not all boiler plate code.  Time spent early on in the life of a project constructing some templates and applying a naming convention to them that can be easily guessed (removing the obstacle of having to remember complex template names) will very quickly be paid back by a combination of increased efficiency and a common standard.