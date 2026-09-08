const nomePokemon = document.querySelector("#nome-pokemon");
const mensagem = document.querySelector("#mensagem");
const detalhes = document.querySelector("#detalhes");
const nome = new URLSearchParams(window.location.search).get("name");
const API_URL = "http://localhost:5000/api/pokemon";

async function carregarPokemon() {
  if (!nome) {
    mensagem.textContent = "Nenhum Pokémon foi escolhido.";
    nomePokemon.textContent = "Pokédex";
    return;
  }

  try {
    const resposta = await fetch(`${API_URL}/name/${encodeURIComponent(nome)}`);

    if (!resposta.ok) {
      throw new Error("Pokémon não encontrado");
    }

    const pokemon = await resposta.json();
    nomePokemon.textContent = pokemon.name;
    document.title = `${pokemon.name} | Pokédex`;
    document.querySelector("#numero").textContent = `#${pokemon.pokedexNumber}`;
    document.querySelector("#altura").textContent = pokemon.height;
    document.querySelector("#peso").textContent = pokemon.weight;
    document.querySelector("#experiencia").textContent = pokemon.baseExperience;
    document.querySelector("#tipos").textContent = pokemon.types.join(", ");

    mensagem.hidden = true;
    detalhes.hidden = false;
  } catch (erro) {
    mensagem.textContent = "Não foi possível carregar este Pokémon.";
    nomePokemon.textContent = "Pokémon não encontrado";
  }
}

carregarPokemon();
