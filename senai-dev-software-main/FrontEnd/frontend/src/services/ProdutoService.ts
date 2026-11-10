
import api from './api'
import type { Produto, NovoProduto } from '../types/Produto'

export const produtoService = {

  // Listar produtos
  listar: async (): Promise<Produto[]> => {
    const { data } = await api.get('/produto')
    return data
  },

  // Buscar produto por ID
  buscarPorId: async (id: number): Promise<Produto> => {
    const { data } = await api.get(`/produto/${id}`)
    return data
  },

  // Cadastrar produto
  criar: async (p: NovoProduto): Promise<Produto> => {
    const { data } = await api.post('/produto', p)
    return data
  },

  // Atualizar produto
  atualizar: async (
    id: number,
    p: NovoProduto
  ): Promise<Produto> => {
    const { data } = await api.put(`/produto/${id}`, p)
    return data
  },

  // Enviar foto
  uploadFoto: async (id: number, arquivo: File): Promise<string> => {
    const formData = new FormData()
    formData.append('arquivo', arquivo)

    const { data } = await api.post(
      `/produto/${id}/foto`,
      formData
    )

    return data.fotoBase64
  },

  // Remover foto
  removerFoto: async (id: number): Promise<void> => {
    await api.delete(`/produto/${id}/foto`)
  }
}
