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

let getReport figures details =
    let ccySum = List