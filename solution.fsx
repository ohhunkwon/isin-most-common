#r "nuget: FSharp.Data"

open FSharp.Data

let file = "/BV_TWEM_20220531_1514.csv"

let csvFile = CsvFile.Load(__SOURCE_DIRECTORY__ + file, skipRows=6, ignoreErrors=true)

let colIndex = csvFile.GetColumnIndex "ISIN"

let addElement m element = 
    m |> Map.change element (Option.defaultValue 0 >> (+) 1 >> Some)

csvFile.Rows
|> Seq.toArray
|> Array.map (fun csvRow -> csvRow.[colIndex]) 
|> Array.fold addElement Map.empty
|> Map.toArray
|> Array.sortBy snd
|> Array.iter (fun (k,v) -> printfn "%s: %d" k v) 
