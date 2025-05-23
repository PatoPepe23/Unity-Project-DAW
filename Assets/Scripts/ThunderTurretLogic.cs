using System.Collections;
using System.Collections.Generic;
using System.Linq; // Necesario para .First() y .RemoveAll()
using UnityEngine;
// Quitado 'using Unity.Mathematics;' si no es estrictamente necesario,
// ya que 'UnityEngine.Quaternion' es la estándar para Instantiate.
// Quitado 'using System.ComponentModel;', 'System.Security.Cryptography.X509Certificates;', etc. si no se usan
// Quitado 'using TMPro;' y 'using UnityEngine.UI;' si no se usan directamente en este script

public class ThunderTurretLogic : MonoBehaviour
{
    // Para interacción de UI (como ya lo tenías)
    public static ThunderTurretLogic SelectedTurret { get; private set; }
    public event System.Action OnTurretClicked;

    // Estadísticas de la torre (como ya las tenías)
    public string turretName;
    public int turretLevel;
    public int level = 1;
    public float moveSpeed = 5f; // Ten en cuenta que esto podría no usarse si el "rayo" no viaja.
    public float cooldownTime = 1f;
    public GameObject bullet; // Este será tu prefab de efecto/rayo

    // No se usa si el rayo aparece directamente en el enemigo
    // public Vector2 position;

    // Lista de enemigos en rango (como ya la tenías)
    private List<Collider2D> enemies = new List<Collider2D>();

    // Start se llama una vez al inicio
    void Start()
    {
        // Si el "rayo" aparece en el enemigo, esta posición no es el punto de origen del rayo.
        // Si necesitas un punto de origen visual para el rayo, puedes crear un Transform hijo.
        // position = new Vector2(transform.position.x, transform.position.y + 1f);
        StartCoroutine(Shoot());
    }

    // Update y FixedUpdate quedan como estaban si no tienen lógica específica
    void Update()
    {
        //
    }

    void FixedUpdate()
    {
        //
    }

    // Detección de colisiones y triggers
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Esta lógica es para que las balas de la torreta no colisionen con la torreta misma.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Tu lógica aquí si necesitas interactuar con enemigos continuamente en el rango.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Solo añadir si no está ya en la lista (redundante con RemoveAll, pero buena práctica)
            if (!enemies.Contains(collision))
            {
                 enemies.Add(collision);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Asegúrate de que el enemigo que sale es el que quieres remover, no siempre el primero.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(collision); // Remover la instancia específica que salió.
        }
    }

    // Coroutine de Disparo
    IEnumerator Shoot()
    {
        while (true) // Bucle de disparo continuo
        {
            // Limpiar la lista de enemigos nulos (destruidos) al inicio de cada ciclo de disparo
            enemies.RemoveAll(enemyCollider => enemyCollider == null);

            if (enemies.Count > 0)
            {
                // No uses try-catch para un flujo normal, esconde errores lógicos.
                // Asegúrate de que tu lógica de manejo de la lista sea robusta.

                GameObject enemyTarget = enemies.First().gameObject;

                // Verificar de nuevo por si el enemigo se destruyó justo después de .First()
                if (enemyTarget != null && enemyTarget.CompareTag("Enemy"))
                {
                    Vector2 enemyPosition = enemyTarget.transform.position;

                    // --- Lógica CLAVE: Spawnea la "bala" (efecto) directamente en la posición del enemigo ---
                    // Instancia el prefab 'bullet' (tu efecto de rayo/impacto) en la posición del enemigo.
                    // Quaternion.identity significa que el efecto se instanciará sin rotación inicial.
                    GameObject thunderEffectInstance = Instantiate(bullet, enemyPosition, Quaternion.identity);

                    // ***** IMPORTANTE *****
                    // Si este "rayo" debe infligir daño:
                    // La lógica para aplicar daño al enemigo (ej. enemyTarget.GetComponent<Health>().TakeDamage(towerDamage);)
                    // DEBERÍA ir aquí o ser manejada por un script en el propio 'bullet' (el efecto)
                    // que detecte la colisión O que se active al instanciarse.
                    // Si el 'bullet' es solo un efecto visual, no necesita Rigidbody2D ni movimiento.

                    // Si el 'bullet' (efecto) tiene un Rigidbody2D y no quieres que se mueva,
                    // puedes desactivar su gravedad o asegurarte de que su Rigidbody2D.bodyType sea Static o Kinematic.
                    // bulletInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Si quieres que no se mueva

                    // Destruye el efecto después de un corto tiempo, ajusta la duración de tu animación.
                    Destroy(thunderEffectInstance, 0.5f); // 0.5 segundos es un ejemplo.
                }
                // No se necesita 'else' para el try-catch aquí, ya que RemoveAll limpia la lista.
            }
            yield return new WaitForSeconds(cooldownTime); // Esperar antes del siguiente disparo
        }
    }

    // Funciones OnMouseDown, SelectTurret, DeselectTurret y OpenTurretMenu como las tenías,
    // asumiendo que UpdateTowerMenu.Instance.ShowMenu existe y recibe los parámetros correctos.
    public void OnMouseDown()
    {
        if (SelectedTurret != null && SelectedTurret != this)
        {
            SelectedTurret.DeselectTurret();
        }
        SelectedTurret = this;
        SelectTurret();
        OnTurretClicked?.Invoke();

        if (UpdateTowerMenu.Instance != null)
        {
            // Asegúrate que UpdateTowerMenu.Instance.ShowMenu(string, int) exista y reciba estos parámetros.
            UpdateTowerMenu.Instance.ShowMenu(turretName, turretLevel);
        }
        else
        {
            Debug.LogError("Error: UpdateTowerMenu.Instance is NULL. Asegúrate de que el script UpdateTowerMenu esté en un GameObject activo y se inicialice correctamente en su método Awake.", this);
        }
    }

    public void SelectTurret()
    {
        Debug.Log("Torreta seleccionada: " + gameObject.name);
        // Agrega aquí retroalimentación visual de selección (ej: activar un sprite de resalte)
    }

    public void DeselectTurret()
    {
        Debug.Log("Torreta deseleccionada: " + gameObject.name);
        SelectedTurret = null;
        // Remueve aquí retroalimentación visual de selección
    }

    // Esta función está vacía, puedes eliminarla si no la usas.
    void OpenTurretMenu()
    {
        
    }
}