module TradeReport

type TradeDetails = {
    id: int
    cashFlows: CashFlow list
}
and TradeReport = {
    year: int
    id: int
    currency: string
    cashFlow: decimal
    cashFlowDetails: CashFlow list
    cashFlowDifference: CashFlowDifference
}
and CashFlowDifference =
    | Match
    | Difference of decimal
    | CurrencyMismatch
and CashFlow = {
    currency: string
    amount: decimal
    date: System.DateTime
}

type TradeFigures = {
    year: int
    id: int
    currency: string
    cashFlow: decimal
}

let getPeriod year =
    (System.DateTime(year, 1, 1), System.DateTime(year, 12, 31))

let inPeriod (s, e) date =
    date >= s && date <= e

let getReport figures details =
    let periodCashFlows =
        details.cashFlows
        |> List.filter (fun cf -> inPeriod (getPeriod figures.year) cf.date)
    let ccySum =
        periodCashFlows
        |> List.groupBy (fun cf -> cf.currency)
        |> List.map (fun (ccy, cfs) -> (ccy, List.sumBy (fun cf -> cf.amount) cfs))
        |> List.filter (fun (_, cf) -> cf <> 0M)
    let cashFlowDifference =
        match ccySum with
        | [(ccy, cfs)] when ccy = figures.currency ->
            if cfs = figures.cashFlow then
                Match
            else
                Difference (figures.cashFlow - cfs)
        | _ -> CurrencyMismatch
    { year = figures.year; id = figures.id; currency = figures.currency; cashFlow = figures.cashFlow; cashFlowDetails = periodCashFlows; cashFlowDifference = cashFlowDifference; }
