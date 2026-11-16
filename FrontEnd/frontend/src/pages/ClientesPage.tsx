
import { useEffect, useState } from 'react'
import type { Cliente } from '../types/Cliente'
import { clienteService } from '../services/ClienteService'
import ClienteForm from '../components/ClienteForm'
import ClienteList from '../components/ClienteList'
import '../assets/css/Clientes.css'

function ClientesPage() {
  const [clientes, setClientes] = useState<Cliente[]>([])
  const [loading, setLoading] = useState(false)
  const [busca, setBusca] = useState('')

  const carregarClientes = async () => {
    setLoading(true)
    try {
      setClientes(await clienteService.listar())
    } catch (error) {
      console.error('Erro ao carregar clientes:', error)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarClientes()
  }, [])

  const clientesFiltrados = clientes.filter((cliente) =>
    `${cliente.nome} ${cliente.email} ${cliente.cpf}`
      .toLowerCase()
      .includes(busca.toLowerCase())
  )

  return (
    <main className="clientes-page">
      <header className="clientes-header">
        <h1>👥 Gestão de Clientes</h1>
        <p>Cadastre, consulte e gerencie os clientes da sua empresa.</p>
      </header>

      <section className="clientes-resumo">
        <article className="cliente-card">
          <span className="icone">👥</span>
          <div>
            <h2>{clientes.length}</h2>
            <p>Total de clientes</p>
          </div>
        </article>

        <article className="cliente-card">
          <span className="icone">✓</span>
          <div>
            <h2>{clientes.filter((c) => c.ativo).length}</h2>
            <p>Clientes ativos</p>
          </div>
        </article>
      </section>

      <section className="clientes-painel">
        <h2>＋ Cadastrar Cliente</h2>
        <ClienteForm onClienteCriado={carregarClientes} />
      </section>

      <section className="clientes-painel">
        <div className="clientes-lista-header">
          <h2>👥 Clientes cadastrados</h2>

          <input
            type="search"
            placeholder="Buscar cliente..."
            value={busca}
            onChange={(e) => setBusca(e.target.value)}
            aria-label="Buscar cliente"
          />
        </div>

        {loading ? (
          <p className="clientes-vazio">Carregando clientes...</p>
        ) : clientesFiltrados.length === 0 ? (
          <p className="clientes-vazio">
            Nenhum cliente encontrado.
          </p>
        ) : (
          <ClienteList
            clientes={clientesFiltrados}
            loading={loading}
          />
        )}
      </section>
    </main>
  )
}

export default ClientesPage
