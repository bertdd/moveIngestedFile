* ingestsubfolders.ada
* 
* calculates the reference to files in sub folders in the file system
* this assumes that the priref of the record is used in the new file name.
* there will be 500 files per sub folder. 
*
* the file name after ingest is in field RF
* in our example we use prirefs from 200.000.000 until 299.999.999
* divided by 500 this maps to 400.000 until 599.999, we subtract 399.999 to obtain a folder range from 1 - 200.000
*
* Author: Bert Degenhart Drenth, Double Dig IT
* Copyright: public domain

    integer folder

    folder = int(val(before$(1, RF, '.'))) / 500   /* 500 files per sub folder
    if (folder <> 0)
    {
      RF = (folder - 399999) + '/' + RF            /* subtract 399.999 to set the first folder to 1.
    }

  end