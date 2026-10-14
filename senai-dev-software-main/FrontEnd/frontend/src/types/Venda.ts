
export interface Venda {
  id: number
  data_venda: String
  quantidade: number
  ValorTotal: number
  
}

export type NovoVenda =
  Omit<Venda, 'id' | 'ativo'>
