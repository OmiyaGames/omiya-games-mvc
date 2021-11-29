# [Omiya Games](https://omiyagames.com) - MVC

[![License Badge](https://img.shields.io/github/license/OmiyaGames/omiya-games-mvc)](/LICENSE.md) [![MVC Package documentation](https://github.com/OmiyaGames/omiya-games-mvc/workflows/Host%20DocFX%20Documentation/badge.svg)](https://omiyagames.github.io/omiya-games-mvc/) [![Ko-fi Badge](https://img.shields.io/badge/donate-ko--fi-29abe0.svg?logo=ko-fi)](https://ko-fi.com/I3I51KS8F)

**Mode-View-Controller (MVC)** is a common way of organizing code for GUI applications.  This package attempts to implement a version of this for Unity scripts.  Currently, this package is in development stages, and only the [Model](https://omiyagames.github.io/omiya-games-mvc/manual/model.html) part of the MVC has been created.

## Install

There are two common methods for installing this package.

### Through [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

Unity's own Package Manager supports [importing packages through a URL to a Git repo](https://docs.unity3d.com/Manual/upm-ui-giturl.html):

1. First, on this repository page, click the "Clone or download" button, and copy over this repository's HTTPS URL.  
2. Then click on the + button on the upper-left-hand corner of the Package Manager, select "Add package from git URL..." on the context menu, then paste this repo's URL!

While easy and straightforward, this method has a few major downside: it does not support dependency resolution and package upgrading when a new version is released.  To add support for that, the following method is recommended:

### Through [OpenUPM](https://openupm.com/)

*Note: this package hasn't been uploaded to OpenUPM yet.  Instructions below are for future references.*

Installing via [OpenUPM's command line tool](https://openupm.com/) is recommended because it supports dependency resolution, upgrading, and downgrading this package.  Given this package is just an example, thought, it hadn't been added into OpenUPM yet.  So the rest of these instructions are hypothetical...for now...

If you haven't already [installed OpenUPM](https://openupm.com/docs/getting-started.html#installing-openupm-cli), you can do so through Node.js's `npm` (obviously have Node.js installed in your system first):
```
npm install -g openupm-cli
```
Then, to install this package, just run the following command at the root of your Unity project:
```
openupm add com.omiyagames.mvc
```

## Resources

- [Documentation](https://omiyagames.github.io/omiya-games-mvc/)
- [Change Log](/CHANGELOG.md)

## LICENSE

Overall package is licensed under [MIT](/LICENSE.md), unless otherwise noted in the [3rd party licenses](/THIRD%20PARTY%20NOTICES.md) file and/or source code.

Copyright (c) 2021 Omiya Games
