[![Build Status](https://travis-ci.org/desperate-man/FileRandomizer3000.svg?branch=master)](https://travis-ci.org/desperate-man/FileRandomizer3000)

A tool for randomizing MP3 files and transferring them to a destination folder, can be handy for MP3 players, smartphones or Digital Music Changer for your car (Yatour etc.).

## Features
* Supply single source folder;
* Find unique or non-unique files;
* Find files only inside selected folder or inside its nested folders;
* Limit files by:
  * total size;
  * number;
  * number per each folder;
* Randomized files can be renamed to keep their naming random in the destination folder;
* Randomized files can be compared to the existing files in the destination folder and you can have several options on what should be done with such files when they are encountered:
  * Skip
  * Overwrite
  * Add prefix or suffix
* Randomized files can be sorted by their name in ascending or descending order.
* Each step of the wizard can be memorized;

## To do
* Multiple source folders;
* Compare files by:
  * ~~file name~~;
  * ID3 tags;
* Preview of randomized files;
* Information after the copy has completed;

## Technology
C#, .NET 4.5.2, WinForms + MVP, [TreeViewFolderBrowser](https://github.com/ChrisRichner/TreeViewFolderBrowser)

## Requirements
OS Windows, .NET >=4.5.2