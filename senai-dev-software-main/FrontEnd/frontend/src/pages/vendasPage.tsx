
import { useEffect, useState } from 'react'
import type { Venda } from '../types/Venda'
import { vendaService } from '../services/vendaService'
import VendaForm from '../components/VendaForm'
import VendaList from '../components/vendasList'
import '../assets/css/Vendas.css'

function VendasPage() {
  const [vendas, setVendas] = useState<Venda[]>([])
  const [loading, setLoading] = useState(false)

  const carregarVenda = async () => {
    setLoading(true)

    try {
      setVendas(await vendaService.listar())
    } catch (error) {
      console.error('Erro ao carregar vendas:', error)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarVenda()
  }, [])

  return (
    <main className="vendas-page">
      <header className="vendas-header">
        <h1>🛍️ Gestão de Vendas</h1>
        <p>
          Selecione um cliente, produto e informe a quantidade
          para registrar uma nova venda.
        </p>
      </header>

      <section className="vendas-formulario">
        <h2>＋ Realizar Venda</h2>
        <VendaForm onVendaCriado={carregarVenda} />
      </section>

      <section className="vendas-resumo">
        <article className="vendas-card">
          <span className="vendas-card-icone">🛒</span>
          <div>
            <h3>Total de Vendas</h3>
            <strong>{vendas.length}</strong>
            <p>registros realizados</p>
          </div>
        </article>

        <article className="vendas-card">
          <span className="vendas-card-icone">📦</span>
          <div>
            <h3>Produtos Distintos</h3>
            <strong>
              {new Set(vendas.map((v) => v.idproduto)).size}
            </strong>
            <p>produtos vendidos</p>
          </div>
        </article>

        <article className="vendas-card">
          <span className="vendas-card-icone">👥</span>
          <div>
            <h3>Clientes Distintos</h3>
            <strong>
              {new Set(vendas.map((v) => v.idcliente)).size}
            </strong>
            <p>clientes atendidos</p>
          </div>
        </article>

        <article className="vendas-card">
          <span className="vendas-card-icone">📊</span>
          <div>
            <h3>Total de Itens</h3>
            <strong>
              {vendas.reduce(
                (total, venda) => total + Number(venda.quantidade || 0),
                0
              )}
            </strong>
            <p>itens vendidos</p>
          </div>
        </article>
      </section>

      <section className="vendas-lista">
        <div className="vendas-lista-header">
          <h2>Vendas Registradas</h2>
        </div>

        {loading ? (
          <p className="vendas-vazio">Carregando vendas...</p>
        ) : (
          <VendaList Venda={vendas} loading={loading} />
        )}
      </section>
    </main>
  )
}

export default VendasPage
