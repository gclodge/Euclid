# Euclid

*Euclid* is a C# (.NET6) project that exposes APIs for working with LiDAR data in the form of ASPRS LAS files.  

## Structure
- **/src/Euclid**
    - C# / .NET6 library that exposes APIs for reading, writing, and editing ASPRS LAS files
- **/src/Euclid.Cli**
    - .NET Core CLI that exposes configurable LAS parsing & processing pipelines
- **/test/Euclid.Tests**
    - Contains unit tests covering the source within the Euclid project

# Roadmap
## Euclid
- Finish LasHeaderBuilder
- Add interfaces for LasWriter
- Add interfaces for LasWriterBuilder
## Euclid.Tests
- Generate fixture dataset of **small** LAS files of known type & point record format
- Add test coverage of LasReader for all above-added test files
## Euclid.Cli
- Add argument parsing & mode selection
- Add simple 'boundary' mode to find and output LAS bounding boxes in GeoJSON 
## Euclid.Cli.Tests
- Actually make it, essentially integration tests but also validate argument parsing