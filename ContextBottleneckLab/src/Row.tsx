import { calculateScore } from './data'
import type { Item } from './data'
import { useRowMeasurement } from './metrics'

export default function Row({ item, toggle }: { item: Item; toggle: (id: number) => void }) {
  useRowMeasurement()
  const score = calculateScore(item.id, item.selected)
  return <li>
    <label>
      <input type="checkbox" checked={item.selected} onChange={() => toggle(item.id)} />
      {item.name}
    </label>
    <span>Score: {score}</span>
  </li>
}
