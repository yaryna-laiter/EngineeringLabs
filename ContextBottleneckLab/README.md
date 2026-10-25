# Context Bottleneck & Component Isolation

A minimal React page with an implementation toggle, performance metrics, 600 checkbox rows, and a short explanation. Baseline and optimized implementations remain in separate files.

## Run

```powershell
cd ContextBottleneckLab
npm.cmd ci
npm.cmd run dev
```

Open the URL printed by Vite. Use the development server for React DevTools profiling.

## Code

- `src/Baseline.tsx`: every row consumes the entire list Context. One checkbox change renders all 600 rows.
- `src/Optimized.tsx`: separate state and action Contexts. A lightweight list passes stable item objects and a stable callback to memoized rows. Only the changed heavy row renders; the list and provider still update.
- `src/Row.tsx`: shared checkbox and expensive calculation.
- `src/data.ts`: creates items and replaces only the changed item object on toggle.
- `src/App.tsx`: switches between implementations and resets selection.

## React DevTools proof

1. Open browser developer tools and React DevTools **Profiler**.
2. Choose **Baseline** and wait for the initial mount.
3. Start recording, click one checkbox, then stop. Inspect the update commit: all 600 `Row` components render.
4. Export the recording and take a screenshot.
5. Choose **Optimized**, wait for mounting, and record one checkbox click again. Only the clicked memoized `Row` renders; the other rows are skipped (gray in the flamegraph).
6. Export the second recording and take a matching screenshot.

Both implementations render 600 rows on mount. Compare checkbox updates. StrictMode and React Compiler are intentionally omitted. The metrics section counts committed heavy row renders and displays React Profiler actualDuration (render execution time, not total interaction latency). Initial mount renders all 600 rows; a checkbox click renders 600 baseline rows or one optimized row. Metrics subscribe outside the measured tree, and their stable Context does not trigger row renders. All 600 rows stay mounted inside the scrollable list. Capture the required recordings directly in React DevTools.

## Checks

```powershell
npm.cmd test
npm.cmd run lint
npm.cmd run build
```

Tests count expensive row calculation calls: 600 baseline renders versus one fixed render, including repeated first, middle, and last item toggles. Browser DevTools recordings remain a manual deliverable.

