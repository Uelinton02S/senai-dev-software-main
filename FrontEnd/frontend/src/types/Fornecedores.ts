
export interface Fornecedores {
  id: number
  nome: string
  email: string
  cnpj: string
  data_venda: string
  cep: number
  numero: number
  complemento: string
  celular: string
  telefone: string
  ativo: boolean
}
export type NovoFornecdor = Omit<Fornecedores, 'id'>