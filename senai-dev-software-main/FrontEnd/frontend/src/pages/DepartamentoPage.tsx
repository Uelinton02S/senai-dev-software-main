import { useEffect, useState } from 'react'
import type { Departamento } from '../types/Departamento'
import { DepartamentoService } from '../services/DepartamentoService'
import DepartamentoForm from '../components/DepartamentoForm'
import DepartamentoList from '../components/DepartamentoList'

function DepartamentoPage() {
  const [Departamento, setDepartamento] = useState<Departamento[]>([])
  const [loading, setLoading] = useState(false)

  const carregarDepartamentos = async () => {
    setLoading(true)
    try { setDepartamento(await DepartamentoService.listar()) }
    finally { setLoading(false) }
  }

  useEffect(() => { carregarDepartamentos() }, [])
  return (<div>
    <h1>Gestão de Departamentos</h1>
    <DepartamentoForm DepartamentoCriado={carregarDepartamentos} />
    <DepartamentoList Departamento={Departamento} loading={loading} />
  </div>)
}
export default DepartamentoPage