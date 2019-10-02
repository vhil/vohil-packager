# Sitecore Package Generator or Pintle.Packager

Sitecore Package Generator or Pintle.Packager is a Sitecore CMS module which provides automation capabilities to generate pre-configred Sitecore CMS installation packages (modules)

This may be helpful for you if 
 * you have a sitecore installation package you need to generate periodically
 * or you are developing a sitecore module and need to generate the installation package when new changes are implemented

The module is a NuGet package that can be used in your solution:
 * [Pintle.Packager](https://www.nuget.org/packages/Pintle.Packager "Pintle.Packager")

# Getting started

## Install nuget package

`Install-Package Pintle.Packager` nuget package into your solution

## Configuration

1. Once nuget package installed, create respective configurations for your packages within `<pintle.packager>` configuration node:
```xml
<sitecore>
  <pintle.packager>
    <packages>
    ...
    </packages>
  </pintle.packager>
</sitecore>
```

2. An example configuration is being shipped with the nuget package within  [MyPackage.config.example](https://github.com/pintle/pintle-packager/blob/master/src/Pintle.Packager/App_Config/Include/Pintle.Packager/MyPackage.config.example "MyPackage.config.example") file:
```xml
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <pintle.packager>
      <packages>
        <package name="MyPackage" type="Pintle.Packager.Configuration.PackageConfiguration, Pintle.Packager">
          <metadata type="Pintle.Packager.Configuration.MetadataConfiguration, Pintle.Packager">
            <packageName>My package</packageName>
            <author>My company</author>
            <version>1.0.0</version>
            <publisher>My company</publisher>
            <license>@ 2019 My company</license>
            <readme>Readme about my package</readme>
            <comment>My package comments</comment>
            <packageId>MP001</packageId>
            <revision>001</revision>
          </metadata>
          <items hint="raw:AddItem">
            <item database="master" path="/sitecore/templates/Sample"/>
            <item database="master" path="/sitecore/layout/Renderings/Sample"/>
            <item database="master" path="/sitecore/system/Modules/My module" children="false"/>
          </items>
          <files hint="raw:AddFile"> 
            <file path="/default.css"/>
          </files>
        </package>
      </packages>
    </pintle.packager>
  </sitecore>
</configuration>
```

3. Open `{host}/sitecore/admin/packager.aspx` and find your configuration in the UI. Using only one click on Generate package button will do all the work for you.
![Packager UI](/assets/example-screenshot.png?raw=true "Packager UI")

4. Alternatively, you can call an endpoint to generate and download your package:
 * `{host}/packager/generate?packageName=MyPackage` - generates the package and stores it on your sitecore instance server, responds with JSON with download url provided
 * `{host}/packager/generate-file?packageName=MyPackage` - generates the package and responds with file stream to download

`packageName` query string parameter must target the package name in the configuration.

## Requirements

* Sitecore CMS 8.0.0 or later

## Documentation

The module is completely configuration driven. Once installed, all dependencies can be found in Sitecore configuration files, and this is the entry point in case you need to patch it for your needs.
[Pintle.Packager.config](https://github.com/pintle/pintle-packager/blob/master/src/Pintle.Packager/App_Config/Include/Pintle.Packager/Pintle.Packager.config "Pintle.Packager.config"):
```xml
<sitecore>
  <pipelines>
    <initialize>
      <processor type="Pintle.Packager.Pipelines.Initialize.RouteMapRegistration, Pintle.Packager"/>
    </initialize>
    <pintle.buildPackage>
      <processor type="Pintle.Packager.Pipelines.BuildPackage.ValidateRequiredParameters, Pintle.Packager"/>
      <processor type="Pintle.Packager.Pipelines.BuildPackage.SetPackageMetadata, Pintle.Packager"/>
      <processor type="Pintle.Packager.Pipelines.BuildPackage.AddPackageItems, Pintle.Packager"/>
      <processor type="Pintle.Packager.Pipelines.BuildPackage.AddPackageFiles, Pintle.Packager"/>
      <processor type="Pintle.Packager.Pipelines.BuildPackage.SetPackageFilePath, Pintle.Packager"/>
      <processor type="Pintle.Packager.Pipelines.BuildPackage.GeneratePackage, Pintle.Packager"/>
    </pintle.buildPackage>
  </pipelines>
  <settings>
    <setting name="Pintle.PackageStoragePath" value="/packager" />
  </settings>
  <pintle.packager>
    <logger type="Pintle.Packager.Logging.Logger, Pintle.Packager" singleInstance="true">
      <param name="loggerName">PackagerLogger</param>
    </logger>
  </pintle.packager>
  <log4net>
    <appender name="PackagerLogFileAppender" type="log4net.Appender.SitecoreLogFileAppender, Sitecore.Logging">
      <file value="$(dataFolder)/logs/Pintle.Packager.log.{date}.txt"/>
      <appendToFile value="true"/>
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%4t %d{ABSOLUTE} %-5p %m%n"/>
      </layout>
      <encoding value="utf-8"/>
    </appender>
    <logger name="PackagerLogger" additivity="false">
      <level value="DEBUG"/>
      <appender-ref ref="PackagerLogFileAppender"/>
    </logger>
  </log4net>
</sitecore>
```

The module introduces its own new sitecore pipeline `pintle.buildPackage` which serves as a point for extensibility if required.

## Contributing

We love it if you would contribute!

Help us! Keep the quality of feature requests and bug reports high

We strive to make it possible for everyone and anybody to contribute to this project. Please help us by making issues easier to resolve by providing sufficient information. Understanding the reasons behind issues can take a lot of time if information is left out.

Thank you, and happy contributing!