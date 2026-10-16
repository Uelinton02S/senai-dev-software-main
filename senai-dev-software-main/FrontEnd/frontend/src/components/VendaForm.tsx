import { useState, useEffect } from 'react'
import { vendaService } from '../services/vendaService'
import { clienteService } from '../services/ClienteService'
import { produtoService } from '../services/produtoService' 

interface Cliente {
  id: number
  nome: string
}
interface Produto {
  id: number
  nome: string
  preco: number
}

interface Props { onVendaCriado: () => void }

export default function VendaForm({ onVendaCriado }: Props) {
 
  const [clientes, setClientes] = useState<Cliente[]>([])
  const [produtos, setProdutos] = useState<Produto[]>([])
  const [idproduto, setIdproduto] = useState('')
  const [idcliente, setIdcliente] = useState('')
  const [quantidade, setQuantidade] = useState('')
  const [loading, setLoading] = useState(false)
  const [erro, setErro] = useState<string | null>(null)


  useEffect(() => {
    async function carregarDados() {
      try {
        const [dadosClientes, dadosProdutos] = await Promise.all([
          clienteService.listar(),
          produtoService.listar()
        ])
        setClientes(dadosClientes)
        setProdutos(dadosProdutos)
      } catch {
        setErro('Erro ao carregar lista de clientes e produtos.')
      }
    }

    carregarDados()
  }, [])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro(null)

    try {
      setLoading(true)
      await vendaService.criar({ idproduto: Number(idproduto), idcliente: Number(idcliente),
         quantidade: Number(quantidade) 
      })
      onVendaCriado()
    } catch { setErro('Erro ao realizar venda. Tente novamente.') 
    } finally { setLoading(false) }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h2>Realizar Venda</h2>
      {erro && <p style={{ color: 'red' }}>{erro}</p>}


      <label htmlFor="idcliente">Cliente</label>
      <select 
        id="idcliente" 
        value={idcliente} 
        onChange={e => setIdcliente(e.target.value)} 
        required
      >
        <option value="">-- Selecione um cliente --</option>
        {clientes.map(cliente => (
          <option key={cliente.id} value={cliente.id}>
            {cliente.nome}
          </option>
        ))}
      </select>

     
      <label htmlFor="idproduto">Produto</label>
      <select 
        id="idproduto" 
        value={idproduto} 
        onChange={e => setIdproduto(e.target.value)} 
        required
      >
        <option value="">--Selecione um produto--</option>
        {produtos.map(produto => (
          <option key={produto.id} value={produto.id}>
            {produto.nome}
          </option>
        ))}
      </select>

  
      <label htmlFor="quantidade">Quantidade</label>
      <input id="quantidade" type="number" min="1"value={quantidade}
        onChange={e => setQuantidade(e.target.value)} 
        required 
      />

      <button disabled={loading}>
        {loading ? 'Salvando...' : 'Cadastrar'}
      </button>
    </form>
  )
}