
import { useState, useEffect } from 'react'
import type { FormEvent } from 'react'
import { useParams } from 'react-router-dom'

import { produtoService } from '../services/produtoService'
import FotoUpload from '../components/Produtos/FotoUpload'

interface Props {
  onProdutoCriado: () => void
}

function ProdutoForm({ onProdutoCriado }: Props) {
  const { id } = useParams<{ id: string }>()
  const modoEdicao = Boolean(id)

  const [nome, setNome] = useState('')
  const [preco, setPreco] = useState('')
  const [estoque, setEstoque] = useState(0)

  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const [fotoBase64Atual, setFotoBase64Atual] =
    useState<string | null>(null)

  // Carregar produto para edição
  async function carregarProduto() {
    if (!id) return

    try {
      const produto = await produtoService.buscarPorId(Number(id))

      setNome(produto.nome)
      setPreco(String(produto.preco))
      setEstoque(produto.estoque)
      setFotoBase64Atual(produto.fotoBase64 ?? null)
    } catch (error) {
      console.error('Erro ao carregar produto:', error)
      setErro('Não foi possível carregar o produto.')
    }
  }

  useEffect(() => {
    if (modoEdicao) {
      carregarProduto()
    }
  }, [id])

  // Cadastrar ou atualizar produto
  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    setErro(null)

    if (!nome.trim() || !preco || Number(preco) <= 0) {
      setErro('Informe um nome e um preço válido.')
      return
    }

    try {
      setLoading(true)

      const dados = {
        nome: nome.trim(),
        preco: Number(preco),
        estoque,
        ativo: true,
      }

      if (modoEdicao && id) {
        await produtoService.atualizar(Number(id), dados)
      } else {
        await produtoService.criar(dados)

        setNome('')
        setPreco('')
        setEstoque(0)
      }

      onProdutoCriado()

    } catch (error) {
      console.error('Erro ao salvar produto:', error)
      setErro('Erro ao salvar. Verifique a API e tente novamente.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="produto-form-container">

      <form onSubmit={handleSubmit}>

        <h2>
          {modoEdicao ? 'Editar Produto' : 'Cadastrar Produto'}
        </h2>

        {erro && (
          <p className="produto-erro">{erro}</p>
        )}

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

        <div>
          <label htmlFor="estoque">Estoque</label>

          <input
            id="estoque"
            type="number"
            min="0"
            value={estoque}
            onChange={(e) => setEstoque(Number(e.target.value))}
            required
          />
        </div>

        <button type="submit" disabled={loading}>
          {loading
            ? 'Salvando...'
            : modoEdicao
              ? 'Atualizar Produto'
              : 'Cadastrar Produto'}
        </button>

      </form>

      {/* Upload da foto somente na edição */}
      {modoEdicao && id && (
        <FotoUpload
          produtoId={Number(id)}
          fotoBase64Atual={fotoBase64Atual}
          onAtualizar={carregarProduto}
        />
      )}

    </div>
  )
}

export default ProdutoForm
