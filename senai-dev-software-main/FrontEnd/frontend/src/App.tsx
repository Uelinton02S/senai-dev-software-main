import { useEffect, useState } from 'react'
import type { Produto } from './types/Produto'
import { produtoService } from './services/produtoService'
import ProdutoForm from './components/ProdutoForm'
import ProdutoList from './components/ProdutoList'
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import Sidebar from './components/SideBar'
import ClientesPage from './pages/ClientesPage'


function ProdutosPage() {
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)

  const carregarProdutos = async () => {
    try {
      setLoading(true)
      const dados = await produtoService.listar()
      setProdutos(dados)
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
    <div>
      <h1>Gestão de Produtos</h1>
      <ProdutoForm onProdutoCriado={carregarProdutos} />
      <h2>Produtos Cadastrados</h2>
      {erro && <p style={{ color: 'red' }}>{erro}</p>}
      <ProdutoList produtos={produtos} loading={loading} />
    </div>
  )
}


function App() {
  return (
    <BrowserRouter>
      <div style={{ display: 'flex' }}>
        <Sidebar />
        <main style={{ flex: 1, padding: '24px' }}>
          <Routes>
            <Route path="/" element={<Navigate to="/produtos" replace />} />
            <Route path="/produtos" element={<ProdutosPage />} />
            <Route path="/clientes" element={<ClientesPage />} />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  )
}

export default App
