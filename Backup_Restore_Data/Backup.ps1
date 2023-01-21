#!/bin/bash

$date = Get-Date -Format "dd/MM/yyyy"



# Create a backup of the Ski database and its Orders collection
mongodump --db Ski --collection Orders --out "C:\Users\Administrator\OneDrive - ipso! Bildung\Schule\Modul 165\Projektarbeit\Backup\Ski_Orders_$date"

