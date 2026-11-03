
import { useEffect, useState } from 'react'
import type { Produto } from '../types/Produto'
import { produtoService } from '../services/produtoService'
import ProdutoForm from '../components/ProdutoForm'
import '../assets/css/Produto.css'

type ProdutoDetalhado = Produto & {
  ativo?: boolean
  estoque?: number
}

function ProdutosPage() {
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [loading, setLoading] = useState(false)
  const [busca, setBusca] = useState('')

  async function carregarProdutos() {
    try {
      setLoading(true)
      const dados = await produtoService.listar()
      setProdutos(dados)
    } catch (erro) {
      console.error('Erro ao carregar produtos:', erro)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarProdutos()
  }, [])

  const detalhados = produtos as ProdutoDetalhado[]

  const filtrados = detalhados.filter((produto) =>
    produto.nome.toLowerCase().includes(busca.toLowerCase())
  )

  const ativos = detalhados.filter((p) => p.ativo === true).length
  const inativos = detalhados.filter((p) => p.ativo === false).length

  const valorEstoque = detalhados.reduce(
    (total, p) =>
      total + Number(p.preco) * Number(p.estoque ?? 0),
    0
  )

  const moeda = (valor: number) =>
    valor.toLocaleString('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    })

  return (
    <main className="produtos-page">
      <header className="produtos-header">
        <div>
          <h1><span>📦</span> Gestão de Produtos</h1>
          <p>Cadastre, consulte e gerencie os produtos da sua empresa.</p>
        </div>
      </header>

      <section className="produtos-resumo">
        <article className="resumo-card azul">
          <span className="resumo-icone">📦</span>
          <div>
            <h2>{produtos.length}</h2>
            <p>Total de produtos</p>
          </div>
        </article>

        <article className="resumo-card verde">
          <span className="resumo-icone">✓</span>
          <div>
            <h2>{ativos}</h2>
            <p>Produtos ativos</p>
          </div>
        </article>

        <article className="resumo-card vermelho">
          <span className="resumo-icone">×</span>
          <div>
            <h2>{inativos}</h2>
            <p>Produtos inativos</p>
          </div>
        </article>

        <article className="resumo-card roxo">
          <span className="resumo-icone">R$</span>
          <div>
            <h2>{moeda(valorEstoque)}</h2>
            <p>Valor estimado em estoque</p>
          </div>
        </article>
      </section>

      <section className="produtos-painel">
        <h2 className="painel-titulo">＋ Cadastrar Produto</h2>
        <ProdutoForm onProdutoCriado={carregarProdutos} />
      </section>

      <section className="produtos-painel">
        <div className="lista-header">
          <h2>📦 Produtos cadastrados</h2>

          <input
            type="search"
            placeholder="⌕  Buscar produto..."
            value={busca}
            onChange={(e) => setBusca(e.target.value)}
            aria-label="Buscar produto"
          />
        </div>

        {loading ? (
          <p className="produtos-mensagem">Carregando produtos...</p>
        ) : filtrados.length === 0 ? (
          <p className="produtos-mensagem">
            Nenhum produto encontrado.
          </p>
        ) : (
          <div className="tabela-container">
            <table className="tabela-produtos">
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Nome</th>
                  <th>Preço</th>
                  <th>Estoque</th>
                  <th>Status</th>
                </tr>
              </thead>

              <tbody>
                {filtrados.map((produto) => (
                  <tr key={produto.id}>
                    <td>{produto.id}</td>
                    <td className="nome-produto">{produto.nome}</td>
                    <td>{moeda(Number(produto.preco))}</td>
                    <td>{produto.estoque ?? '—'}</td>
                    <td>
                      {produto.ativo === undefined ? (
                        <span className="status-indisponivel">
                          Não informado
                        </span>
                      ) : (
                        <span
                          className={
                            produto.ativo
                              ? 'status ativo'
                              : 'status inativo'
                          }
                        >
                          {produto.ativo ? 'Ativo' : 'Inativo'}
                        </span>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}

        <footer className="tabela-rodape">
          Mostrando {filtrados.length} de {produtos.length} produtos
        </footer>
      </section>
    </main>
  )
}

export default ProdutosPage
