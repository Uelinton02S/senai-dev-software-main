
import { useEffect, useState } from 'react'
import type { Produto } from '../types/Produto'
import { produtoService } from '../services/ProdutoService'
import ProdutoForm from '../components/ProdutoForm'
import '../assets/css/Produto.css'

type ProdutoDetalhado = Produto & {
  ativo?: boolean
  estoque?: number
}

function ProdutosPage() {
  const [produtos, setProdutos] = useState<ProdutoDetalhado[]>([])
  const [loading, setLoading] = useState(true)
  const [erro, setErro] = useState('')
  const [busca, setBusca] = useState('')
  const [fotoSelecionada, setFotoSelecionada] = useState<ProdutoDetalhado | null>(null)

  async function carregarProdutos() {
    try {
      setLoading(true)
      setErro('')

      const dados = await produtoService.listar()
      setProdutos(dados as ProdutoDetalhado[])
    } catch (error) {
      console.error('Erro ao carregar produtos:', error)
      setErro('Não foi possível carregar os produtos.')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    carregarProdutos()
  }, [])

  const filtrados = produtos.filter((produto) =>
    produto.nome.toLowerCase().includes(busca.toLowerCase())
  )

  const ativos = produtos.filter(p => p.ativo === true).length
  const inativos = produtos.filter(p => p.ativo === false).length

  const valorEstoque = produtos.reduce(
    (total, p) =>
      total + Number(p.preco) * Number(p.estoque ?? 0),
    0
  )

  function moeda(valor: number) {
    return valor.toLocaleString('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    })
  }

  return (
    <main className="produtos-page">

      <header className="produtos-header">
        <div>
          <h1>📦 Gestão de Produtos</h1>
          <p>
            Cadastre, consulte e gerencie os produtos da sua empresa.
          </p>
        </div>
      </header>

      {/* RESUMO */}
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

      {/* CADASTRO */}
      <section className="produtos-painel">
        <h2 className="painel-titulo">
          ＋ Cadastrar Produto
        </h2>

        <ProdutoForm onProdutoCriado={carregarProdutos} />
      </section>

      {/* LISTAGEM */}
      <section className="produtos-painel">

        <div className="lista-header">
          <h2>📦 Produtos cadastrados</h2>

          <input
            type="search"
            placeholder="⌕ Buscar produto..."
            value={busca}
            onChange={(e) => setBusca(e.target.value)}
            aria-label="Buscar produto"
          />
        </div>

        {loading ? (
          <p className="produtos-mensagem">
            Carregando produtos...
          </p>
        ) : erro ? (
          <p className="produtos-mensagem">
            {erro}
          </p>
        ) : filtrados.length === 0 ? (
          <p className="produtos-mensagem">
            Nenhum produto encontrado.
          </p>
        ) : (

          <div className="tabela-container">

            <table className="tabela-produtos">

              <thead>
                <tr>
                  <th>Foto</th>
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

                    {/* FOTO */}
                    <td>
                      {produto.fotoBase64 ? (
                        <img
                          src={produto.fotoBase64}
                          alt={`Foto de ${produto.nome}`}
                          onClick={() => setFotoSelecionada(produto)}
                          style={{
                            width: 65,
                            height: 65,
                            objectFit: 'cover',
                            borderRadius: 8,
                            cursor: 'pointer'
                          }}
                        />
                      ) : (
                        <div
                          style={{
                            width: 65,
                            height: 65,
                            borderRadius: 8,
                            background: '#eeeeee',
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'center',
                            fontSize: 24
                          }}
                          title="Produto sem foto"
                        >
                          📷
                        </div>
                      )}
                    </td>

                    <td>{produto.id}</td>

                    <td className="nome-produto">
                      {produto.nome}
                    </td>

                    <td>
                      {moeda(Number(produto.preco))}
                    </td>

                    <td>
                      {produto.estoque ?? '—'}
                    </td>

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

      {/* MODAL DA FOTO */}
      {fotoSelecionada && (
        <div
          onClick={() => setFotoSelecionada(null)}
          style={{
            position: 'fixed',
            inset: 0,
            background: 'rgba(0,0,0,0.75)',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            zIndex: 9999,
            padding: 20
          }}
        >
          <div
            onClick={(e) => e.stopPropagation()}
            style={{
              background: '#fff',
              padding: 20,
              borderRadius: 12,
              maxWidth: 500,
              width: '100%',
              textAlign: 'center'
            }}
          >
            <h2>{fotoSelecionada.nome}</h2>

            <img
              src={fotoSelecionada.fotoBase64}
              alt={fotoSelecionada.nome}
              style={{
                width: '100%',
                maxHeight: 400,
                objectFit: 'contain'
              }}
            />

            <button
              onClick={() => setFotoSelecionada(null)}
              style={{ marginTop: 15 }}
            >
              Fechar
            </button>
          </div>
        </div>
      )}

    </main>
  )
}

export default ProdutosPage
