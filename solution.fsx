#r "nuget: FSharp.Data"

open FSharp.Data
open System.Collections.Generic

let file = "/BV_TWEM_20220531_1514.csv"

let csv = CsvFile.Load(__SOURCE_DIRECTORY__ + file, skipRows=6, ignoreErrors=true).Cache()

let dict = new Dictionary<string, int>()

for row in csv.Rows do
    // printfn "ISIN: (%s)"
    //     (row.GetColumn "ISIN")
    let isin = row.GetColumn "ISIN"
    if not (dict.ContainsKey(isin)) then dict.Add(isin, 1) else dict.[isin] <- dict.[isin] + 1
    
dict
|> Seq.sortBy (fun (KeyValue(k,v)) -> v)
|> Seq.iter (fun (KeyValue(k,v)) -> printfn "%s: %d" k v)