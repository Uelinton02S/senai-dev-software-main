// src/types/Produto.ts
// ⚠️ Os nomes devem ser IDÊNTICOS ao que o Swagger retorna!
// Verifique: "nome" ou "Nome"? "preco" ou "Preco"?

export interface Produto {
  Id: number
  Nome: string
  Preco: number
}

// Tipo para criação — sem o id (gerado pela MinhaAPI)
export type NovoProduto = Omit<Produto, 'id'>