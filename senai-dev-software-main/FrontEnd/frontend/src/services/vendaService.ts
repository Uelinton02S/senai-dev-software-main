
import api from './api'
import type { Venda, NovoVenda } from '../types/Venda'

export const vendaService = {
  listar: async (): Promise<Venda[]> => {
    const { data } = await api.get('/venda')
    return data
  },

  criar: async (v: NovoVenda): Promise<Venda> => {
    const { data } = await api.post('/venda', v)
    return data
  }
}
