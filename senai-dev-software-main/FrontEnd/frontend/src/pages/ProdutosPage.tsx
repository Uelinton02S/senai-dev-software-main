
import { useEffect, useState } from 'react'
import type { Produto } from '../types/Produto'
import { produtoService } from '../services/produtoService'
import ProdutoForm from '../components/ProdutoForm'
import ProdutoList from '../components/ProdutoList'
import '../assets/css/Produtos.css'

function ProdutosPage() {
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const carregarProdutos = async () => {
    try {
      setLoading(true)
      setErro(null)
      setProdutos(await produtoService.listar())
    } catch {
      setErro('Erro ao carregar produtos.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarProdutos()
  }, [])

  return (
    <div className="produtos-container">
      <header className="produtos-header">
        <h1>Gestão de Produtos</h1>
        <p>Cadastre, consulte e gerencie os produtos da sua empresa.</p>
      </header>

      <section className="produto-card">
        <h2>📦 Cadastrar Produto</h2>
        <ProdutoForm onProdutoCriado={carregarProdutos} />
      </section>

      {erro && (
        <p className="produtos-erro" role="alert">
          {erro}
        </p>
      )}

      <section className="produtos-section">
        <h2>Produtos cadastrados</h2>
        <ProdutoList produtos={produtos} loading={loading} />
      </section>
    </div>
  )
}

export default ProdutosPage
