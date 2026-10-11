import { useEffect, useState } from 'react'
import './App.css'

// read the API base URL from the environment
const API = import.meta.env.VITE_API_URL

function App() {
  const [items, setItems] = useState([])
  const [error, setError] = useState('')

  useEffect(() => {
    fetch(`${API}/api/Menu`)
      .then((res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}`)
        return res.json()
      })
      .then(setItems)
      .catch((err) => setError(err.message))
  }, [])

  return (
    <div style={{ padding: '2rem', textAlign: 'left' }}>
      <h1>CampusEats Menu</h1>
      <p>API: {API || 'VITE_API_URL is not set'}</p>
      {error && <p style={{ color: 'crimson' }}>Could not load menu: {error}</p>}
      <ul>
        {items.map((m) => (
          <li key={m.id}>
            {m.name} - Rs. {m.price} ({m.category})
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App