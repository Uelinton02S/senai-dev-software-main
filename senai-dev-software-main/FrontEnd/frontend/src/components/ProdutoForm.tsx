
import { useState } from 'react'
import type { FormEvent } from 'react'
import { produtoService } from '../services/produtoService'

interface Props {
  onProdutoCriado: () => void
}

function ProdutoForm({ onProdutoCriado }: Props) {
  const [nome, setNome] = useState('')
  const [preco, setPreco] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    setErro(null)

    if (!nome.trim() || !preco || Number(preco) <= 0) {
      setErro('Informe um nome e um preço válido.')
      return
    }

    try {
      setLoading(true)

      await produtoService.criar({
        nome: nome.trim(),
        preco: Number(preco),
        estoque: 0,
        ativo: true,
      })

      setNome('')
      setPreco('')
      onProdutoCriado()
    } catch (error) {
      console.error('Erro ao cadastrar produto:', error)
      setErro('Erro ao cadastrar. Verifique a API e tente novamente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      {erro && <p className="produto-erro">{erro}</p>}

      <div>
        <label htmlFor="nome">Nome</label>
        <input
          id="nome"
          type="text"
          placeholder="Ex.: Notebook"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
        />
      </div>

      <div>
        <label htmlFor="preco">Preço (R$)</label>
        <input
          id="preco"
          type="number"
          min="0.01"
          step="0.01"
          placeholder="Ex.: 3500,00"
          value={preco}
          onChange={(e) => setPreco(e.target.value)}
          required
        />
      </div>

      <button type="submit" disabled={loading}>
        {loading ? 'Salvando...' : 'Cadastrar'}
      </button>
    </form>
  )
}

export default ProdutoForm
