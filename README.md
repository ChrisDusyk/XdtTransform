XdtTransform
============

## Summary

Apply XDT transforms outside of Visual Studio.

## Usage

The below is a demonstration outlining how the application may be called and what the inputs/outputs might look like.

    XdtTransform --source SOURCE_WEB_CONFIG ^
                 --transform TRANSFORM_WEB_CONFIG ^
                 --destination DESTINATION_WEB_CONFIG

**"SOURCE_WEB_CONFIG"** (e.g. `$(ProjectDir)\web.config`)

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
      <connectionStrings>
        <add name="default" connectionString="productionConnectionString" />
      </connectionStrings>
    </configuration>

**"TRANSFORM_WEB_CONFIG"** (e.g. `$(ProjectDir)\web.$(Configuration).config`)

    <?xml version="1.0" encoding="utf-8"?>
    <configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
      <connectionStrings>
        <add name="default" connectionString="debugConnectionString"
             xdt:Transform="Replace" xdt:Locator="Match(name)"/>
      </connectionStrings>
    </configuration>

**"DESTINATION_WEB_CONFIG"** (e.g. `$(OutDir)\web.config`)

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
      <connectionStrings>
        <add name="default" connectionString="debugConnectionString" />
      </connectionStrings>
    </configuration>
