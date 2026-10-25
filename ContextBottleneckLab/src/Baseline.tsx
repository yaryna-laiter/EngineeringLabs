import { createContext, useContext, useState } from 'react'
import { createItems, toggleItem } from './data'
import type { Item } from './data'
import Row from './Row'

const ListContext = createContext<{ items: Item[]; toggle: (id: number) => void } | null>(null)

function BaselineRow({ id }: { id: number }) {
  // Every row consumes the entire list, so any checkbox updates all 600 rows.
  const { items, toggle } = useContext(ListContext)!
  return <Row item={items[id]} toggle={toggle} />
}

export default function Baseline() {
  const [items, setItems] = useState(createItems)
  const toggle = (id: number) => setItems(previous => toggleItem(previous, id))

  return <ListContext.Provider value={{ items, toggle }}>
    <ul>{items.map(item => <BaselineRow key={item.id} id={item.id} />)}</ul>
  </ListContext.Provider>
}
