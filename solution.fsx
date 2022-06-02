#r "nuget: FSharp.Data"

open FSharp.Data

let file = "/BV_TWEM_20220531_1514.csv"

let csv = CsvFile.Load(__SOURCE_DIRECTORY__ + file, skipRows=6, ignoreErrors=true)

csv.Rows
|> Seq.countBy (fun csvRow -> csvRow.GetColumn "ISIN")
|> Seq.sortBy snd
|> Seq.iter (fun (k, v) -> printfn "%s: %d" k v)
