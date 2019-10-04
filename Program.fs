// Learn more about F# at http://fsharp.org

open System
open TradeReport

let details1 = { TradeDetails.id = 1; cashFlows = [ { CashFlow.amount = 100M; CashFlow.currency = "DKK"; date = DateTime(2019, 6, 1); } ] }
let figures1 = { TradeFigures.id = 1; year = 2019; currency = "DKK"; cashFlow = 100M; }

let details2 = { TradeDetails.id = 2; cashFlows =
    [ { CashFlow.amount = 100M; CashFlow.currency = "DKK"; date = DateTime(2019, 6, 1); };
      { CashFlow.amount = 100M; CashFlow.currency = "SEK"; date = DateTime(2019, 6, 1); }; ]; }
let figures2 = { TradeFigures.id = 2; year = 2019; currency = "DKK"; cashFlow = 100M; }

let details3 = { TradeDetails.id = 2; cashFlows =
    [ { CashFlow.amount = 100M; CashFlow.currency = "DKK"; date = DateTime(2019, 6, 1); };
      { CashFlow.amount = 100M; CashFlow.currency = "DKK"; date = DateTime(2019, 6, 1); };
      { CashFlow.amount = 100M; CashFlow.currency = "SEK"; date = DateTime(2018, 6, 1); }; ]; }
let figures3 = { TradeFigures.id = 2; year = 2019; currency = "DKK"; cashFlow = 100M; }

[<EntryPoint>]
let main argv =
    let reportData = [ (details1, figures1); (details2, figures2); (details3, figures3); ]
    let reports =
        reportData
        |> List.map (fun (d, f) -> getReport f d)
    0 // return an integer exit code
