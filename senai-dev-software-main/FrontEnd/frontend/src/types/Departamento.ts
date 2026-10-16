export interface Departamento {
  id: number
  nome: string
  email: string
  idfornecedores: number,
  iddepartamento: number,
  telefone : string,
  descricao: string
  ativo: boolean
}

export type NovoDepartamento =
  Omit<Departamento, 'id' | 'ativo'>