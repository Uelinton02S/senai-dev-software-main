
import { useEffect, useState } from 'react'
import type { Venda } from '../types/Venda'
import type { Produto } from '../types/Produto'
import { produtoService } from '../services/produtoService'

interface Props {
  Venda: Venda[]
  loading: boolean
}

function VendasList({ Venda, loading }: Props) {
  const [produtos, setProdutos] = useState<Produto[]>([])

  useEffect(() => {
    produtoService.listar()
      .then(setProdutos)
      .catch((erro) => {
        console.error('Erro ao carregar produtos:', erro)
      })
  }, [])

  if (loading) {
    return <p className="vendas-vazio">Carregando vendas...</p>
  }

  if (Venda.length === 0) {
    return (
      <p className="vendas-vazio">
        Nenhuma venda realizada ainda.
      </p>
    )
  }

  return (
    <ul>
      {Venda.map((v) => {
        const produto = produtos.find(
          (p) => Number(p.id) === Number(v.idproduto)
        )

        return (
          <li key={v.id}>
            <strong>
              {produto?.nome ?? `Produto #${v.idproduto}`}
            </strong>
            {' — '}
            {produto
              ? `R$ ${Number(produto.preco).toFixed(2).replace('.', ',')}`
              : `ID ${v.idproduto}`}
            <span> (quantidade: {v.quantidade})</span>
          </li>
        )
      })}
    </ul>
  )
}

export default VendasList
