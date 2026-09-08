const form = document.querySelector("#form-tipo");
const inputTipo = document.querySelector("#tipo");
const mensagem = document.querySelector("#mensagem");
const listaPokemon = document.querySelector("#lista-pokemon");

// O backend usa esta porta, definida em BackEnd/Properties/launchSettings.json.
const API_URL = "http://localhost:5000/api/pokemon";

form.addEventListener("submit", async (event) => {
  event.preventDefault(); // Evita que a página seja recarregada.

  const tipo = inputTipo.value.trim().toLowerCase();
  listaPokemon.innerHTML = "";
  mensagem.textContent = "A procurar Pokémon...";

  try {
    const resposta = await fetch(`${API_URL}/type/${encodeURIComponent(tipo)}`);

    if (!resposta.ok) {
      throw new Error("Tipo não encontrado");
    }

    const dados = await resposta.json();
    mensagem.textContent = `${dados.count} Pokémon do tipo ${dados.type}:`;

    dados.pokemons.forEach((nome) => {
      const item = document.createElement("li");
      item.textContent = nome;
      listaPokemon.appendChild(item);
    });
  } catch (erro) {
    mensagem.textContent = "Não foi possível encontrar Pokémon desse tipo.";
  }
});
