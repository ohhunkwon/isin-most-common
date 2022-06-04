#r "nuget: FSharp.Data"

open FSharp.Data

let file = "/BV_TWEM_20220531_1514.csv"

let csv = CsvFile.Load(__SOURCE_DIRECTORY__ + file, skipRows=6, ignoreErrors=true)

// csv.Rows
// |> Seq.countBy (fun csvRow -> csvRow.GetColumn "ISIN")
// |> Seq.sortBy snd
// |> Seq.iter (fun (k, v) -> printfn "%s: %d" k v)
let csvIsin = Seq.map (fun (csvRow : CsvRow) -> csvRow.GetColumn "ISIN") csv.Rows

let csvRowArr = Seq.toArray csvIsin

let addElement (acc:Map<string,int>) element = 
    if acc.ContainsKey element then acc.Add(element, acc.[element] + 1) 
    else acc.Add(element, 1)

(Map.empty, csvRowArr)
||> Seq.fold (addElement)
|> Seq.sortBy (fun (KeyValue(k,v)) -> v)
|> Seq.iter (fun (KeyValue(k,v)) -> printfn "%s: %d" k v) 

