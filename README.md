BioTekLiquidHandler
===================

VWorks plugin for controlling BioTek liquid handlers (e.g. ELx405, MultiFlo, MultiFloFX).

A very simple plugin (consider using as a template project if you're building a new VWorks plugin). The differences compared to Agilent's plugin are only minor:
* No XML RPC servers to run
* No association of an LHC protocol folder with device profile (easier for our protocol file organization method)

Installation
------------
Copy the following files to the VWorks Plugin directory:

* BioTekLiquidHandler.dll
* BioTekLiquidHandler.tlb
* LHC_Installation_Folder.inf (from the Liquid Handling Control installation directory)
* BTILHCRunner.dll (from the Liquid Handling Control installation directory or from here; versions must match)
