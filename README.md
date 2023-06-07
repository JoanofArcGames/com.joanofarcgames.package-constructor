# Important statement

### This package is by no means stable or production-ready
It might break under unpredictable circumstances. It might not contain features you expect. It might contain bugs
<br />If you encounter any issues, if you have features you'd want included, if you've found a bug, please, ***file a GitHub issue***

# Overview

This package is a full-featured solution to automate creation of new Unity packages.
It carefully follows [recommended package layout](https://docs.unity3d.com/2022.2/Documentation/Manual/cus-layout.html).
The goal is to reduce friction and number of boilerplate-kind of actions when creating new Unity packages.
The constructor does nothing more than is outlined in [Unity's instructions](https://docs.unity3d.com/2022.2/Documentation/Manual/CustomPackages.html)
on creating custom packages. It just does it automatically, saving your valuable time

# Installation

### Brief

Install via git url:
<br /> `https://github.com/JoanofArcGames/com.joanofarcgames.package-constructor.git`

### Detailed

1. Open Unity Package Manager
2. In top-left corner, click `+`
3. Choose `Add package from git URL...`
4. Paste git url of this package:
<br /> `https://github.com/JoanofArcGames/com.joanofarcgames.package-constructor.git`
5. Click `Add` / press `Enter`

# How to use

- In `/Assets/` directory, `right click` – `create` – `JAG` – `Construction config`
- Set up the config. Use the guide below for reference
- Click `Construct` button at the bottom of config file's inspector
  - If config contains invalid data, errors will be reported in Console window, and construction will be aborted

Note: to maximize time you save by using this package, it is recommended to keep all your packages in a single project with Package Constructor installed and configured

# Config guide

## Package information, Dependencies, Keywords and Author sections

This is what goes to your `package.json` file, which is the core of every Unity package.
It contains all valuable information about the package,
such as name, version, description, dependencies, links to changelog, documentation, etc.
Read more in [Unity Manual](https://docs.unity3d.com/Manual/upm-manifestPkg.html)

### Package information

- `Version` – This is current version of your package. It **must** comply with **semantic versioning** pattern.
  - [Unity Manual page about semantic versioning](https://docs.unity3d.com/2022.3/Documentation/Manual/upm-semver.html)
  - [Semantic versioning official website](https://semver.org)
<br /><br />

- `Company Name` and `Package Name` – Name of your company and your package. They **must** be PascalCase.
  - Note: you can specify additional nested namespaces divided with `.` in `Package Name`
  - Note: `Company Name` and `Package Name` will get automatically combined and converted into [reverse domain name notation](https://en.wikipedia.org/wiki/Reverse_domain_name_notation),
  such as <nobr>`com.unity.entities`</nobr> or <nobr>`com.unity.burst`</nobr>
  <br />For example, `MyAwesomeCompany` and `MyAwesomePackage` will result in <nobr>`com.my-awesome-company.my-awesome-package`</nobr>
  <br />See [Unity Manual](https://docs.unity3d.com/Manual/cus-naming.html) for package naming conventions
<br /><br />

- `Display Name` – User-friendly name to appear in Unity editor, such as `Entities` or `Burst`
<br /><br />

- `Description` – A brief description of the package. This is the text that appears in the details view of the Package Manager window.
This field supports `UTF–8` character codes.
<br /><br />

- `Unity Version` and `Unity Release` – The lowest Unity version compatible with the package.
<br />`Unity Version` is `<MAJOR>.<MINOR>`, such as `2022.3`,
<br />`Unity Release` is `<UPDATE>.<RELEASE>`, such as `0f1`
  - Note: if these fields are omitted, Package Manager considers your package compatible with all Unity versions
  - Note: if `Unity Version` is omitted, `Unity Release` will make no effect
  - Note: a package that isn't compatible with Unity doesn't appear in Package Manager window
<br /><br />

- `Documentation URL`, `Changelog URL` and `Licenses URL` – You can specify links to web pages containing related information about you package
<br /><br />

- `License` – Identifier for an OSS license using the [SPDX identifier format](https://spdx.org/licenses), or a string such as `See LICENSE.md file`
<br /><br />

- `Hide In Editor` – Whether or not to hide your package in Editor. Most packages have this set to `true`

### Dependencies

Specify packages that your package depends on.
Use official names in [reverse domain name notation](https://en.wikipedia.org/wiki/Reverse_domain_name_notation)
and [semver](https://semver.org) versions

### Keywords

Keywords are used by the Package Manager search APIs. This helps users find relevant packages

### Author

Specify information related to either you personally, or your company
<br />Note: if you fill either `email` or `url`, the `name` field **must** be filled as well

## Directories

### `/Editor/`

Put your code/assets that are specific to Unity editor in this directory.
<br />Assembly definition which is constructed by default is configured to only include Editor as platform,
meaning code you put there will not ship with built game. For example, Package Constructor's files are in this directory

### `/Runtime/`

Put your code/assets that are supposed to work anywhere besides Unity editor in this directory

### `/Tests/Editor/` and `/Tests/Runtime/`

Tests for code in `/Editor/` and `/Runtime/` directories, correspondingly
<br />See [Unity Manual](https://docs.unity3d.com/Manual/cus-tests.html) for details

### `/Samples~/`

Directory to include samples, if any
See [Unity Manual](https://docs.unity3d.com/Manual/cus-samples.html) for details

### `/Documentation~/`

Most packages require some form of documentation
See [Unity Manual](https://docs.unity3d.com/Manual/cus-document.html) for details

## Markdown files

### `README.md`

This is where you write overview of your package, installation instructions, notes, screenshots, etc.
For example, this text is part of a README file
<br />[Markdown syntax cheat sheet](https://www.markdownguide.org/cheat-sheet)
<br />[Examples of readme files](https://github.com/matiassingers/awesome-readme)

### `CHANGELOG.md`

This is version history of your package
<br />[Your git log is not a changelog](https://agateau.com/2022/your-git-log-is-not-a-changelog)
<br />[Keep a changelog](https://keepachangelog.com/en/1.1.0)

### `LICENSE.md`

This is where you put your license in form of plain text or a link to appropriate web page
<br />[Read about software licensing](https://en.wikipedia.org/wiki/Software_license)
<br />[SPDX License List](https://spdx.org/licenses)
<br />[Meeting legal requirements](https://docs.unity3d.com/Manual/cus-legal.html)

### `Third Party Notices.md`

In case your package is using third-party libraries or assets of any kind, you might have to include their licenses
<br />[Read more about third party notices in Unity Manual](https://docs.unity3d.com/2022.2/Documentation/Manual/cus-legal.html#:~:text=terms%20and%20conditions.-,Third%20Party%20Notices,-If%20your%20package)

## Miscellaneous

### `Include template documentation`

This option is available only if you have selected to include `/Documentation~/` directory
<br />Provides you with a template documentation file which complies with [Unity's guidelines](https://docs.unity3d.com/Manual/cus-document.html)

### `Include blank scripts`

Generates a blank `.cs` script alongside each `.asmdef` file. Otherwise Unity will complain in console that you have assemblies with no scripts associated

### `Enable logging`

Display informational messages in console regarding construction. This doesn't affect config validation errors

# Developer notes

- Currently, I am unaware of how to disable warnings regarding asmdefs with no scripts associated, other than including placeholder scripts.
If you have ideas, communicate them to me
- Currently, constructor doesn't add entry for samples into `package.json`.
It doesn't make sense to do so, because when you just start your package, you don't have any samples.
Probably I will add functionality to auto-generate entries for samples and inject them to existing `package.json`