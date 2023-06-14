export interface User {
    IdAzienda? : number
    AccountAzienda : string
    PswAzienda : string
    NomeAzienda? : string
    SaldoAzienda? : number
    TokenPersonale? : CoppiaToken
}

interface CoppiaToken {
    AccessToken : string,
    RefreshToken : string
}