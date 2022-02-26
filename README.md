# Foolishly Sparked

[![wakatime](https://wakatime.com/badge/github/BinaryMisfit/foolishly-sparked.svg)](https://wakatime.com/badge/github/BinaryMisfit/foolishly-sparked)

*This project is in no way associated with Electronic Arts and the all trademarks for The Sims belong to Electronic
Arts.*

## Important Notice

> Recently there was a strong discussion whether a project like this is needed with some of the more established mod authors in the Sims community. Quite a few points was made that led to the project being made private to consider the arguments. After some serious consideration this project is still open source and available here. No public or official release will be done nor will it be made available in compiled form.

## Overview

Foolishly Sparked provides an updated set of tools to manage and maintain Sims custom content files. It's built on .Net
Core 6.0 and a refresh of the work done by various other [projects](#reference-projects).

## Goal

Managing content for the Sims can be hard. If using content from the gallery everything is done for you. When using
content from other creators it can become more challenging and difficult to figure out. Some of the challenges with
managing custom content include but not limited to:

- Duplicate content.
- Multiple overrides.
- Load order of content.
- Game compatibility.
- Content updates.

To address these issues numerous tools provide functionality often to complex or to simple for what needs to be
achieved. After spending time trying to work through 5000+ custom content items the need for something to automate or
batch some of these functions became a must and Foolishly Sparked was born.

Foolishly Sparked attempts to bring all the tools and functionality into one place across both Windows and Mac.

## Planned Functionality

| Function       | Purpose                                             | Status |    Roadmap    |
|:---------------|:----------------------------------------------------|:------:|:-------------:|
| Game Detection | Locate and find the installed instance of the game. | In Dev | Sunset Valley |

## Golden Rules

While developing Foolishly Sparked there are a set of rules applied in deciding how something gets implemented and if it
meets the goal of the project.

### Keep it Simple

Seems like an obvious rule but it harder to implement then it sounds. All features need to meet some criteria and
understanding of implementation.

- Should be available via command line.
- Should be a plugin.
- Should have a singular purpose.
- Should support single and batch mode.
- Should work on both Windows and Mac.

## Current Status

| Last Update | Details                           |
|:-----------:|:----------------------------------|
| 2021-02-17  | Populating and updating README.md |
|             | Initial attempt at a Roadmap      |
| 2021-02-24  | Renamed to Foolishly Sparked      |

## Roadmap

|   Codename    | Release | Feature                                              |  Expected   |
|:-------------:|:-------:|:-----------------------------------------------------|:-----------:|
| Sunset Valley |  0.0.1  | Command line game detection and reporting.           | 4 - 6 Weeks |
|               |         | Command line custom content detection and reporting. |             |
|               |         | Initial Plugin Framework                             |             |

## Reference Projects

This project wouldn't be possible without the work done in the following projects and teams. Most of the code around
dealing with the internals of Sims `.package` files was taken from these projects.

- ### Sims4Tools
    - [Kuree](https://github.com/Kuree/Sims4Tools) - the original Sims 4 Tools.
    - [cmarNYC](https://github.com/cmarNYC/Sims4Tools) - the first maintained fork.
    - [s4ptacle](https://github.com/s4ptacle/Sims4Tools) - the last maintained fork.
- ### [S4 CAS Tools](https://modthesims.info/d/582348/s4-cas-tools-updated-to-v3-5-3-1-on-9-11-2021.html) by cmarNYC.
- ### [s3pi](http://s3pi.sourceforge.net/) by Peter.
- ### [Sims 4 Toolkit](https://sims4toolkit.com/) by frankkulamods.

#### Interesting Facts

- <sub>The original name for this project was Sims Toolkit. However after learning about Sims 4 Toolkit predating this
  project it was renamed to Foolishly Sparked. I felt it was only respectful as the work on Sims 4 Toolkit started long
  before this project.</sub>
- <sub>Most of the referenced project's authors have been privately contacted regarding the use of their code where
  possible.</sub>
