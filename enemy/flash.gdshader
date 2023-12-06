shader_type canvas_item;

uniform vec4 flash_color : source_color = vec4(1.0);
uniform float flash_opacity : hint_range(0.0, 1.0, 0.1) = 0.0;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec4 color = texture(TEXTURE,UV);
	color.rgb = mix(color.rgb,flash_color.rgb,flash_opacity);
	COLOR = color;
}

void light() {
	// Called for every pixel for every light affecting the CanvasItem.
}
