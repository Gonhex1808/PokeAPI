const form = document.querySelector("#form-tipo");
const inputPesquisa = document.querySelector("#pesquisa");
const mensagem = document.querySelector("#mensagem");
const listaPokemon = document.querySelector("#lista-pokemon");

// O backend usa esta porta, definida em BackEnd/Properties/launchSettings.json.
const API_URL = "http://localhost:5000/api/pokemon";

form.addEventListener("submit", async (event) => {
  event.preventDefault(); // Evita que a página seja recarregada.

  const pesquisa = inputPesquisa.value.trim().toLowerCase();
  listaPokemon.innerHTML = "";
  mensagem.textContent = "A procurar Pokémon...";

  try {
    // Primeiro interpreta a pesquisa como tipo: "fire", "water", etc.
    const respostaTipo = await fetch(`${API_URL}/type/${encodeURIComponent(pesquisa)}`);

    if (respostaTipo.ok) {
      const dados = await respostaTipo.json();
      mensagem.textContent = `${dados.count} Pokémon do tipo ${dados.type}:`;
      mostrarLista(dados.pokemons);
      return;
    }

    // Se não for um tipo, procura o mesmo texto como nome de Pokémon.
    const respostaPokemon = await fetch(`${API_URL}/name/${encodeURIComponent(pesquisa)}`);

    if (!respostaPokemon.ok) {
      throw new Error("Pesquisa não encontrada");
    }

    const pokemon = await respostaPokemon.json();
    mensagem.textContent = `Pokémon encontrado: ${pokemon.name}`;
    mostrarLista([
      `N.º ${pokemon.pokedexNumber}`,
      `Altura: ${pokemon.height}`,
      `Peso: ${pokemon.weight}`,
      `Tipos: ${pokemon.types.join(", ")}`
    ]);
  } catch (erro) {
    mensagem.textContent = "Não foi encontrado um Pokémon nem um tipo com esse nome.";
  }
});

function mostrarLista(nomes) {
  nomes.forEach((nome) => {
    const item = document.createElement("li");
    const botao = document.createElement("button");
    botao.type = "button";
    botao.textContent = nome;
    item.appendChild(botao);
    listaPokemon.appendChild(item);
  });
}
