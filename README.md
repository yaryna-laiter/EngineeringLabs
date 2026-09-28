# Engineering Labs

A collection of small practical labs focused on understanding runtime behavior, execution mechanics, diagnostics, and performance in .NET and React.

## Labs

### 1. Async Deadlock & Thread Pool Starvation (.NET)

Demonstrates the impact of blocking asynchronous code with `.Result` under concurrent load.

The lab compares:

- Blocking async code
- Fully asynchronous `async/await`
- Thread Pool behavior under load
- Performance before and after the fix

Tools used include load testing and .NET diagnostics.

---

### 2. Memory Leak Hunt (.NET)

Demonstrates an intentional memory leak and how to diagnose object retention using memory profiling tools.

The lab covers:

- Creating an intentional memory leak
- Simulating application load
- Inspecting GC roots and object retention
- Fixing the leak
- Comparing memory usage before and after the fix

---

### 3. Stale Closure & Custom Hook Challenge (React)

Demonstrates how JavaScript closures interact with React renders and long-lived subscriptions.

The lab contains:

- A deliberately broken `useEventSocket` hook
- A visible stale closure bug
- A fixed implementation using `useRef`
- A stable subscription that can still access the latest React state

The mock socket is implemented using `setInterval`.

---

### 4. EF Core Query Optimization & SQL Auditing (.NET)

Demonstrates how different LINQ loading strategies can generate very different SQL and database behavior.

The lab compares:

- Lazy loading and the N+1 query problem
- Eager loading with `Include()` and Cartesian explosion
- DTO projection with `.Select()`
- Database roundtrips
- Generated SQL
- Retrieved data shape
- Execution time

The lab uses EF Core SQL logging and a command interceptor to inspect and compare actual database execution.

---

## Goal

Build practical knowledge of:

- Async execution and Thread Pool behavior
- Garbage Collection and memory diagnostics
- JavaScript lexical scope and closures
- React component lifecycle and hooks
- EF Core query translation and SQL generation
- Lazy loading, eager loading, and DTO projection
- N+1 queries and Cartesian explosion
- Database roundtrips and query performance
- Debugging and profiling runtime problems

Each lab includes a small demo showing the problem, investigation, fix or optimization, and resulting behavior.