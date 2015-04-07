XdtTransform
============

### Summary
This project allows you to apply [XDT transformations][1] outside of VisualStudio. This is particularly useful in Sitecore solutions when you need to apply settings outside of the Sitecore's `<sitecore>` node in the root web.config.

### Example

The below is a _pseudo-post-build_ script demonstrating how it _may_ be performed.
      
    XdtTransform --source "$(SitecoreDir)\web.config" ^
                 --transform "$(ProjectDir)\web.$(Configuration).config" ^
                 --destination "$(OutDir)\web.config"

### Build Configuration

* **Debug**  
  Builds to an EXE and runs a Post-Build step showing an example transformation in the output window.
* **Release**  
  Builds to an EXE.
* **Library**  
  Builds to a DLL.

  [1]: https://msdn.microsoft.com/en-us/library/dd465326.aspx