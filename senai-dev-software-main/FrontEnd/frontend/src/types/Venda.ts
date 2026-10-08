// Os nomes devem coincidir com o Swagger
export interface Venda {
  id: number
  data_venda: number
  quantidade: number
  ValorTotal: number
  
}

export type NovoCliente =
  Omit<Venda, 'id' | 'ativo'>