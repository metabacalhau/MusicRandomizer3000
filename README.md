[![Build Status](https://travis-ci.org/desperate-man/MusicRandomizer3000.svg?branch=master)](https://travis-ci.org/desperate-man/MusicRandomizer3000)

A tool for randomizing audio files and transferring them to a destination folder.

## Features
* Supply various source folders with recursion or without
* Find unique or non-unique files
* Find files only inside selected folder or inside its nested folders
* Limit files by:
  * total size
  * number
  * number per each folder (can be handy for Yatour digital music changer or similar)
* Randomized files can be renamed to shuffle the files naturally in the destination folder (can be useful for FAT32)
* Randomized files can be sorted by their name in ascending or descending order
* Each step of the wizard can be memorized

## Technology
C#, .NET 4.5.2, WinForms + MVP, [TreeViewFolderBrowser](https://github.com/ChrisRichner/TreeViewFolderBrowser)

## Requirements
OS Windows, .NET >=4.5.2