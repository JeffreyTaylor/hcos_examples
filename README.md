# hcos_examples

hcOS Examples is a collection of code examples and documentation to help you get started using the APIs quickly.

## Development environment

These examples are written in .Net Core but we will be expanding support to other languages in the future.

### Common

Tools                                                  | Notes
-------------------------------------------------------|----------
[Chocolatey](https://chocolatey.org/)                  | Recommended. Allows for cleaner installs/updates.
Chocolatey GUI                                         | Recommended. choco install ChocolateyGUI
[Visual Studio Code](https://code.visualstudio.com/)   | Recommended. Per User install.
GIT                                                    | Required. Admin install for all users. If chocolatey is installed -> choco install git.install
[Docker](https://docs.docker.com/)                     | Recommended for some of the more advance examples.

### .Net Core

Tools                                                  | Notes
-------------------------------------------------------|------------------------------------------------------------------
[.Net Core SDK](https://dotnet.microsoft.com/download) | Required.  Supporting versiom 2.2.104. Admin install for all users.

### Java

Tools                                                                                                | Notes
-----------------------------------------------------------------------------------------------------|--------------------------------
[Java JDK](https://www.oracle.com/technetwork/java/javase/downloads/index-jsp-138363.html#javasejdk) | If using chocolatey -> choco install jdk8

### Node.js

Tools                             | Notes
----------------------------------|----------------------------------------------------
[Node.js](https://nodejs.org/en/) | If using chocolatey -> choco install nodejs.install

### Python

Tools                             | Notes
----------------------------------|-------------------------------------
[Python](https://www.python.org/) | If using chocolatey -> choco install python3

## Data

[PatientDocument.bulk.json](./data/PatientDocument.bulk.json) containing 1000 Mimic PatientDocuments has been provided.

A [folder](./data/text) been provided with extracted MIMIC text reports.  This should help you create search queries of your own to test hcOS search capabilities against our MIMIC tenant.

## Samples

### [Getting Started](./getting_started)
