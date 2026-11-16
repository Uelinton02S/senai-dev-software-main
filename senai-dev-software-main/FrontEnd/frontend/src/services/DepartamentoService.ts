// src/services/produtoService.ts
import api from './api'
import type { Departamento, NovoDepartamento } from '../types/Departamento'

export const DepartamentoService = {

  listar: async (): Promise<Departamento[]> => {
    const { data } = await api.get('/Departamento')
    return data
  },

  criar: async (d: NovoDepartamento): Promise<Departamento> => {
    const { data } = await api.post('/Departamento', d)
    return data
  }
}