
export interface Venda {
  id: number
  idproduto: number
  idcliente: number
  quantidade: number
 
  
}

export type NovoVenda =
  Omit<Venda, 'id' | 'ativo'>
