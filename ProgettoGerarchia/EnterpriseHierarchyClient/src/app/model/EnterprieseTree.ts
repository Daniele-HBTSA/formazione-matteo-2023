export interface EnterprieseTree { 
    Id?: number,
    Code : string,
    Balance : number,
    Selected: boolean,
    Children : EnterprieseTree[]
}
