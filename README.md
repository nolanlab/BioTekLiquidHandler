BioTekLiquidHandler
===================

VWorks plugin for controlling BioTek liquid handlers (e.g. ELx405, MultiFlo, MultiFloFX).

A very simple plugin (consider using as a template project if you're building a new VWorks plugin). The differences compared to Agilent's plugin are only minor:
* No XML RPC servers to run
* No association of an LHC protocol folder with device profile (easier for our protocol file organization method)

Installation
------------
Copy the following files to the VWorks Plugin directory:

* BioTekLiquidHandler.dll from [here](https://github.com/nolanlab/BioTekLiquidHandler/tree/master/BioTekLiquidHandler/bin/Release)
* BioTekLiquidHandler.tlb  from [here](https://github.com/nolanlab/BioTekLiquidHandler/tree/master/BioTekLiquidHandler/bin/Release)
* LHC_Installation_Folder.inf from your Liquid Handling Control installation directory
* BTILHCRunner.dll from your Liquid Handling Control installation directory or from  from [here](https://github.com/nolanlab/BioTekLiquidHandler/tree/master/BioTekLiquidHandler/bin/Release). The DLL version and LHC version must match; the version in this repo is for LHC 2.16.
