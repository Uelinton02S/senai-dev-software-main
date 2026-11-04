import api from './api'
import type { Venda, NovoVenda } from '../types/Venda'

export const vendaService = {
  listar: async (): Promise<Venda[]> => {
    const { data } = await api.get('/venda')
    return data
  },

  criar: async (v: NovoVenda): Promise<Venda> => {
    const payload = {
      idprodutos: Number(v.idproduto || v.idproduto),
      idclientes: Number(v.idcliente || v.idcliente),
      quantidade: Number(v.quantidade)
    }
    const { data } = await api.post('/venda', payload)
    return data
  }
}