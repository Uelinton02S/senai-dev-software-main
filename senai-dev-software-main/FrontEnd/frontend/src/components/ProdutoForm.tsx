
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

  const [arquivoFoto, setArquivoFoto] = useState<File | null>(null)

  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const [fotoBase64Atual, setFotoBase64Atual] =
    useState<string | null>(null)

  async function carregarProduto() {
    if (!id) return

    try {
      const produto = await produtoService.buscarPorId(Number(id))

      setNome(produto.nome)
      setPreco(String(produto.preco))
      setEstoque(produto.estoque)
      setFotoBase64Atual(produto.fotoBase64 ?? null)
    } catch (error) {
      console.error(error)
      setErro('Não foi possível carregar o produto.')
    }
  }

  useEffect(() => {
    if (modoEdicao) {
      carregarProduto()
    }
  }, [id])

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

      let produtoSalvo

      if (modoEdicao && id) {
        produtoSalvo = await produtoService.atualizar(
          Number(id),
          dados
        )
      } else {
        produtoSalvo = await produtoService.criar(dados)
      }

      // Enviar foto depois de salvar o produto
      if (arquivoFoto) {
        await produtoService.uploadFoto(
          produtoSalvo.id,
          arquivoFoto
        )
      }

      setNome('')
      setPreco('')
      setEstoque(0)
      setArquivoFoto(null)

      onProdutoCriado()

      if (modoEdicao) {
        await carregarProduto()
      }

    } catch (error) {
      console.error('Erro ao salvar produto:', error)
      setErro('Erro ao salvar produto ou enviar a foto.')
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

        <div>
          <label htmlFor="foto">Foto do produto</label>
          <input
            id="foto"
            type="file"
            accept="image/*"
            onChange={(e) =>
              setArquivoFoto(e.target.files?.[0] ?? null)
            }
          />
        </div>

        {arquivoFoto && (
          <div>
            <p>Foto selecionada: {arquivoFoto.name}</p>
            <img
              src={URL.createObjectURL(arquivoFoto)}
              alt="Prévia do produto"
              width="120"
            />
          </div>
        )}

        <button type="submit" disabled={loading}>
          {loading
            ? 'Salvando...'
            : modoEdicao
              ? 'Atualizar Produto'
              : 'Cadastrar Produto'}
        </button>

      </form>

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
