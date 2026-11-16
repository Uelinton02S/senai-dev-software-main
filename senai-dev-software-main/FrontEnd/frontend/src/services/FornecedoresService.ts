
import api from './api'
import type { Fornecedores } from '../types/Fornecedores'

const listar = async (): Promise<Fornecedores[]> => {
  const response = await api.get<Fornecedores[]>('/fornecedores')
  return response.data
}

const buscarPorId = async (id: number): Promise<Fornecedores> => {
  const response = await api.get<Fornecedores>(`/fornecedores/${id}`)
  return response.data
}

const criar = async (
  fornecedores: Omit<Fornecedores, 'id'>
): Promise<Fornecedores> => {
  const response = await api.post<Fornecedores>('/fornecedores', fornecedores)
  return response.data
}

const atualizar = async (
  id: number,
  fornecedores: Omit<Fornecedores, 'id'>
): Promise<Fornecedores> => {
  const response = await api.put<Fornecedores>(
    `/fornecedores/${id}`,
    fornecedores
  )
  return response.data
}

const excluir = async (id: number): Promise<void> => {
  await api.delete(`/fornecedores/${id}`)
}

export const fornecedoresService = {
  listar,
  buscarPorId,
  criar,
  atualizar,
  excluir
}
