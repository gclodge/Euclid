# Euclid

This C# (.NET6) project contains the source of the core classes and utilities that make up *Euclid*.  This document is intended to serve as an index as well as a guide for intended usage of the APIs contained herein.

## Namespaces
The library currently contains the following namespaces:
- [Euclid.Las](#euclidlas)
    - Las contains all the functionality for reading, writing, and editing LAS files
- [Euclid.Time](#euclidtime)
    - Time contains helper methods for doing timestamp handling that is common to LAS files (such as second-of-week conversion, UTC to GPS, etc)

# Roadmap
## Euclid
- Finish LasHeaderBuilder
## Euclid.Tests
- Generate fixture dataset of **small** LAS files of known type & point record format
- Add test coverage of LasReader for all above-added test files
- Add test coverage of LasWriter to ensure format adherence
## Euclid.Cli
- Add argument parsing & mode selection
- Add simple 'boundary' mode to find and output LAS bounding boxes in GeoJSON 
## Euclid.Cli.Tests
- Actually make it, essentially integration tests but also validate argument parsing